using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Order;
namespace AgroShopApp.Services.Core
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public OrderService(
            IOrderRepository orderRepo,
            ICartRepository cartRepo,
            IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task PlaceOrderAsync(Guid userId)
        {
            var cart = await _cartRepo.GetWithItemsAsync(userId);

            if (!cart.Items.Any())
                throw new InvalidOperationException("Your cart is empty.");

            var order = new Order
            {
                UserId = userId,
                OrderedOn = DateTime.UtcNow,
                Status = "Pending",
                TotalAmount = 0
            };

            foreach (var item in cart.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);

                if (product == null || product.StockQuantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Not enough stock for '{item.Product.Name}'.");
                }

                product.StockQuantity -= item.Quantity;

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });

                order.TotalAmount += item.Quantity * product.Price;
            }

            await _orderRepo.AddAsync(order);

            cart.Items.Clear();
            await _cartRepo.SaveChangesAsync();
        }
        public async Task<PaginatedOrderListViewModel> GetPaginatedUserOrdersAsync(Guid userId, int page, int pageSize)
        {
            var allOrders = await _orderRepo.GetAllByUserAsync(userId);

            var total = allOrders.Count();
            var paged = allOrders
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginatedOrderListViewModel
            {
                Orders = paged.Select(o => new OrderSummaryViewModel
                {
                    Id = o.Id,
                    OrderedOn = o.OrderedOn,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount
                }).ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize)
            };
        }
        public async Task<OrderDetailsViewModel?> GetDetailsAsync(Guid orderId, Guid userId)
        {
            var order = await _orderRepo.GetWithItemsAsync(orderId);

            if (order == null || order.UserId != userId)
                return null;

            return new OrderDetailsViewModel
            {
                Id = order.Id,
                OrderedOn = order.OrderedOn,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Items = order.Items.Select(i => new OrderItemViewModel
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
        }

    }
}
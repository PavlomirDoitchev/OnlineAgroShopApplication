using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Order;
using AgroShopApp.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<AdminOrderListItemViewModel>> GetFilteredOrdersAsync(OrderFilterInputModel filter)
        {
            var allOrders = await _orderRepo.GetAllWithUserAsync();

            var filtered = allOrders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Email))
                filtered = filtered.Where(o => o.User.Email.Contains(filter.Email));

            if (!string.IsNullOrWhiteSpace(filter.Status))
                filtered = filtered.Where(o => o.Status == filter.Status);

            if (filter.FromDate.HasValue)
                filtered = filtered.Where(o => o.OrderedOn >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                filtered = filtered.Where(o => o.OrderedOn <= filter.ToDate.Value);

            return filtered
                .Select(o => new AdminOrderListItemViewModel
                {
                    Id = o.Id,
                    Email = o.User.Email,
                    OrderedOn = o.OrderedOn,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount
                })
                .ToList();
        }

        public async Task<AdminOrderDetailsViewModel?> GetOrderDetailsAsync(Guid orderId)
        {
            var order = await _orderRepo
                .GetAllAttached()
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return null;

            return new AdminOrderDetailsViewModel
            {
                Id = order.Id,
                Email = order.User.Email,
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
        public async Task<PaginatedAdminOrderListViewModel> GetPaginatedFilteredOrdersAsync(OrderFilterInputModel filter, int page, int pageSize)
        {
            var allOrders = await _orderRepo.GetAllWithUserAsync();
            var query = allOrders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(o => o.User.Email.Contains(filter.Email));

            if (!string.IsNullOrWhiteSpace(filter.Status))
                query = query.Where(o => o.Status == filter.Status);

            if (filter.FromDate.HasValue)
                query = query.Where(o => o.OrderedOn >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(o => o.OrderedOn <= filter.ToDate.Value);

            var totalOrders = query.Count();
            var orders = query
                .OrderByDescending(o => o.OrderedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new AdminOrderListItemViewModel
                {
                    Id = o.Id,
                    Email = o.User.Email,
                    OrderedOn = o.OrderedOn,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount
                })
                .ToList();

            return new PaginatedAdminOrderListViewModel
            {
                Orders = orders,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalOrders / (double)pageSize),
                Filter = filter
            };
        }
        public async Task<bool> UpdateStatusAsync(Guid orderId, string newStatus)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null)
                return false;

            order.Status = newStatus;
            return await _orderRepo.UpdateAsync(order);
        }

    }
}
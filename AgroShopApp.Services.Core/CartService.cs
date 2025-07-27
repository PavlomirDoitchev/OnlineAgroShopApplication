using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Cart;
namespace AgroShopApp.Services.Core
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public CartService(ICartRepository cartRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task AddToCartAsync(string userId, Guid productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null || !product.IsAvailable || product.IsDeleted || product.StockQuantity == 0)
            {
                throw new InvalidOperationException("Product is unavailable or out of stock.");
            }

            var cart = await _cartRepo.GetOrCreateCartAsync(userId);
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                if (existingItem.Quantity < product.StockQuantity)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    throw new InvalidOperationException("Cannot add more items than available stock.");
                }
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = 1
                });
            }

            await _cartRepo.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string userId, Guid productId)
        {
            var cart = await _cartRepo.GetOrCreateCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                cart.Items.Remove(item);
                await _cartRepo.SaveChangesAsync();
            }
        }

        public async Task DecreaseQuantityAsync(string userId, Guid productId)
        {
            var cart = await _cartRepo.GetOrCreateCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    cart.Items.Remove(item);
                }

                await _cartRepo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId)
        {
            var cart = await _cartRepo.GetOrCreateCartAsync(userId);

            return cart.Items
                .Where(ci => ci.Product != null && ci.Quantity > 0)
                .Select(ci => new CartItemViewModel
                {
                    ProductId = ci.ProductId,
                    Name = ci.Product.Name,
                    Price = ci.Product.Price,
                    ImageUrl = ci.Product.ImageUrl,
                    Quantity = ci.Quantity,
                    StockQuantity = ci.Product.StockQuantity,
                    Total = ci.Product.Price * ci.Quantity
                });
        }
        public async Task SetQuantityAsync(string userId, Guid productId, int quantity)
        {
            var cart = await _cartRepo.GetWithItemsAsync(userId);

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                throw new InvalidOperationException("Product not found in cart.");
            }

            cartItem.Quantity = quantity;

            await _cartRepo.UpdateAsync(cart);
        }

        public async Task<int> GetStockForProductAsync(Guid productId)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            return product.StockQuantity;
        }
    }
}

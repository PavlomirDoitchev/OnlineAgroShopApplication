using AgroShopApp.Web.ViewModels.Cart;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, Guid productId);
        Task DecreaseQuantityAsync(string userId, Guid productId);
        Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId);
        Task RemoveFromCartAsync(string userId, Guid productId);
        Task SetQuantityAsync(string userId, Guid productId, int quantity);
        Task<int> GetStockForProductAsync(Guid productId);
        Task<decimal> GetCartTotalAsync(string userId);
        Task<decimal> GetProductPriceAsync(Guid productId);
    }
}
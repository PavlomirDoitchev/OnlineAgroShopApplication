using AgroShopApp.Web.ViewModels.Cart;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, Guid productId);
        Task DecreaseQuantityAsync(string userId, Guid productId);
        Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(string userId);
        Task ClearCartAsync(string userId);
    }
}
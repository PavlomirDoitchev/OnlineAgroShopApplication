using AgroShopApp.Web.ViewModels.Cart;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface ICartService
    {
        Task AddToCartAsync(Guid userId, Guid productId);
        Task DecreaseQuantityAsync(Guid userId, Guid productId);
        Task<IEnumerable<CartItemViewModel>> GetCartItemsAsync(Guid userId);
        Task RemoveFromCartAsync(Guid userId, Guid productId);
        Task SetQuantityAsync(Guid userId, Guid productId, int quantity);
        Task<int> GetStockForProductAsync(Guid productId);
        Task<decimal> GetCartTotalAsync(Guid userId);
        Task<decimal> GetProductPriceAsync(Guid productId);

    }
}
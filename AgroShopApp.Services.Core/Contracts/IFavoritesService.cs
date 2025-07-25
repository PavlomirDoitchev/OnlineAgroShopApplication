using AgroShopApp.Web.ViewModels.Product;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface IFavoritesService
    {
        Task AddToFavoritesAsync(string userId, Guid productId);
        Task RemoveFromFavoritesAsync(string userId, Guid productId);
        Task<IEnumerable<FavoriteProductViewModel>> GetUserFavoritesAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, Guid productId);
    }
}
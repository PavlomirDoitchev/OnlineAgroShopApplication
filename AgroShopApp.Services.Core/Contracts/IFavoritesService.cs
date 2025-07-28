using AgroShopApp.Web.ViewModels.Product;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface IFavoritesService
    {
        Task AddToFavoritesAsync(Guid userId, Guid productId);
        Task RemoveFromFavoritesAsync(Guid userId, Guid productId);
        Task<IEnumerable<FavoriteProductViewModel>> GetUserFavoritesAsync(Guid userId);
        Task<bool> IsFavoriteAsync(Guid userId, Guid productId);
    }
}
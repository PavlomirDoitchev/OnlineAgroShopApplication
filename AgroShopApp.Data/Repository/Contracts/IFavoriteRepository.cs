using AgroShopApp.Data.Models;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface IFavoriteRepository : IAsyncRepository<Favorite, (string UserId, Guid ProductId)>
    {
        Task<IEnumerable<Favorite>> GetUserFavoritesAsync(string userId);
        Task<bool> ExistsAsync(string userId, Guid productId);
    }
}
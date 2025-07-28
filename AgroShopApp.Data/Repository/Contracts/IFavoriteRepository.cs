using AgroShopApp.Data.Models;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface IFavoriteRepository : IAsyncRepository<Favorite, (Guid UserId, Guid ProductId)>
    {
        Task<IEnumerable<Favorite>> GetUserFavoritesAsync(Guid userId);
        Task<bool> ExistsAsync(Guid userId, Guid productId);
    }
}
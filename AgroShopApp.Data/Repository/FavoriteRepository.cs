using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Repository
{
    public class FavoriteRepository : BaseRepository<Favorite, (Guid, Guid)>, IFavoriteRepository
    {
        public FavoriteRepository(AgroShopDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(Guid userId)
        {
            return await DbSet
                .Where(f => f.UserId == userId && !f.Product.IsDeleted)
                .Include(f => f.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid productId)
        {
            return await DbSet.AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }
    }
}

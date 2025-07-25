using AgroShopApp.Data;
using AgroShopApp.Data.Models;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Services.Core
{
    public class FavoriteService : IFavoriteService
    {
        private readonly AgroShopDbContext _context;

        public FavoriteService(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task AddToFavoritesAsync(string userId, Guid productId)
        {
            var exists = await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);

            if (!exists)
            {
                _context.Favorites.Add(new Favorite
                {
                    UserId = userId,
                    ProductId = productId
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromFavoritesAsync(string userId, Guid productId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FavoriteProductViewModel>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Product)
                .ThenInclude(p => p.Category)
                .Select(f => new FavoriteProductViewModel
                {
                    ProductId = f.Product.Id,
                    Name = f.Product.Name,
                    Description = f.Product.Description,
                    Price = f.Product.Price,
                    ImageUrl = f.Product.ImageUrl,
                    Category = f.Product.Category.Name
                })
                .ToListAsync();
        }

        public async Task<bool> IsFavoriteAsync(string userId, Guid productId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);
        }
    }
}

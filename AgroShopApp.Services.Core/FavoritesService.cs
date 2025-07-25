using AgroShopApp.Data;
using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Services.Core
{
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoriteRepository _favoriteRepo;

        public FavoritesService(IFavoriteRepository favoriteRepo)
        {
            _favoriteRepo = favoriteRepo;
        }
        public async Task AddToFavoritesAsync(string userId, Guid productId)
        {
            if (!await _favoriteRepo.ExistsAsync(userId, productId))
            {
                var favorite = new Favorite
                {
                    UserId = userId,
                    ProductId = productId
                };

                await _favoriteRepo.AddAsync(favorite);
            }
        }


        public async Task RemoveFromFavoritesAsync(string userId, Guid productId)
        {
            var favorite = await _favoriteRepo.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);

            if (favorite != null)
            {
                await _favoriteRepo.HardDeleteAsync(favorite);
            }
        }

        public async Task<IEnumerable<FavoriteProductViewModel>> GetUserFavoritesAsync(string userId)
        {
            var favorites = await _favoriteRepo.GetUserFavoritesAsync(userId);

            return favorites.Select(f => new FavoriteProductViewModel
            {
                ProductId = f.Product.Id,
                Name = f.Product.Name,
                Description = f.Product.Description,
                Price = f.Product.Price,
                ImageUrl = f.Product.ImageUrl,
                Category = f.Product.Category.Name
            });
        }

        public async Task<bool> IsFavoriteAsync(string userId, Guid productId)
        {
            return await _favoriteRepo.ExistsAsync(userId, productId);
        }
    }
}

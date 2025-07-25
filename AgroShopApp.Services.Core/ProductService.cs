using AgroShopApp.Data;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Services.Core
{
    public class ProductService : IProductService
    {
        private readonly AgroShopDbContext _context;

        public ProductService(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AllProductsViewModel>> GetAllAsync(int? categoryId = null, string? searchTerm = null, string? userId = null)
        {
            var productsQuery = _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Where(p => p.IsAvailable);

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            var userFavorites = string.IsNullOrEmpty(userId)
                ? new List<Guid>()
                : await _context.Favorites
                    .Where(f => f.UserId == userId)
                    .Select(f => f.ProductId)
                    .ToListAsync();

            return await productsQuery
                .Select(p => new AllProductsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    IsFavorite = userFavorites.Contains(p.Id)
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<ProductCategoryViewModel>> GetCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(c => new ProductCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
        public async Task<AllProductsViewModel?> GetDetailsAsync(Guid id, string? userId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.IsAvailable);

            if (product == null)
            {
                return null;
            }

            var isFavorite = false;

            if (!string.IsNullOrEmpty(userId))
            {
                isFavorite = await _context.Favorites
                    .AnyAsync(f => f.ProductId == product.Id && f.UserId == userId);
            }

            return new AllProductsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category.Name,
                IsFavorite = isFavorite
            };
        }
    }
}

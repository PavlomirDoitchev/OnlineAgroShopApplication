using AgroShopApp.Data;
using AgroShopApp.Data.Models;
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
        public async Task CreateAsync(ProductFormViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                StockQuantity = model.StockQuantity,
                CategoryId = model.CategoryId,
                IsAvailable = true,
                IsDeleted = false,
                AddedOn = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<EditProductViewModel?> GetEditAsync(Guid id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return null;

            var categories = await GetCategoriesAsync();

            return new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                IsDeleted = product.IsDeleted,
                Categories = categories
            };
        }

        public async Task EditAsync(EditProductViewModel model)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
                return;

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.StockQuantity = model.StockQuantity;
            product.CategoryId = model.CategoryId;
            product.IsDeleted = model.IsDeleted;

            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<AllProductsViewModel>> GetDeletedAsync()
        {
            return await _context.Products
                .IgnoreQueryFilters()
                .Include(p => p.Category)
                .Where(p => p.IsDeleted == true)
                .AsNoTracking()
                .Select(p => new AllProductsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                })
                .ToListAsync();
        }
    }
}

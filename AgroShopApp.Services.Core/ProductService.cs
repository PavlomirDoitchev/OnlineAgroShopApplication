using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Services.Core
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IFavoriteRepository favoriteRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<AllProductsViewModel>> GetAllAsync(int? categoryId = null, string? searchTerm = null, string? userId = null)
        {
            var productsQuery = await _productRepository.GetAllWithCategoryAsync();

            var filtered = productsQuery.Where(p => p.IsAvailable && !p.IsDeleted);

            if (categoryId.HasValue)
            {
                filtered = filtered.Where(p => p.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filtered = filtered.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            var userFavorites = string.IsNullOrEmpty(userId)
                ? new List<Guid>()
                : (await _favoriteRepository.GetUserFavoritesAsync(userId)).Select(f => f.ProductId).ToList();

            return filtered.Select(p => new AllProductsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Category = p.Category.Name,
                IsFavorite = userFavorites.Contains(p.Id),
                StockQuantity = p.StockQuantity
            });
        }

        public async Task<IEnumerable<ProductCategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllSortedAsync();

            return categories.Select(c => new ProductCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<AllProductsViewModel?> GetDetailsAsync(Guid id, string? userId)
        {
            var product = await _productRepository.GetWithCategoryByIdAsync(id);

            if (product == null || !product.IsAvailable || product.IsDeleted)
            {
                return null;
            }

            var isFavorite = !string.IsNullOrEmpty(userId) && await _favoriteRepository.ExistsAsync(userId, product.Id);

            return new AllProductsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category.Name,
                StockQuantity = product.StockQuantity,
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

            await _productRepository.AddAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product != null && !product.IsDeleted)
            {
                product.IsDeleted = true;
                product.DeletedOn = DateTime.UtcNow;

                await _productRepository.UpdateAsync(product);
            }
        }

        public async Task<EditProductViewModel?> GetEditAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

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
                Categories = categories
            };
        }

        public async Task EditAsync(EditProductViewModel model)
        {
            var product = await _productRepository.GetByIdAsync(model.Id!.Value);

            if (product == null)
                return;

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.StockQuantity = model.StockQuantity;
            product.CategoryId = model.CategoryId;

            await _productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<DeletedProductViewModel>> GetDeletedDetailedAsync()
        {
            var deletedProducts = await _productRepository.GetDeletedProductsAsync();

            return deletedProducts.Select(p => new DeletedProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Category = p.Category.Name,
                DeletedOn = p.DeletedOn
            });
        }

        public async Task RestoreAsync(Guid id)
        {
            var product = await _productRepository.GetByIdIncludingDeletedAsync(id);

            if (product != null && product.IsDeleted)
            {
                product.IsDeleted = false;
                product.DeletedOn = null;

                await _productRepository.UpdateAsync(product);
            }
        }
    }
}

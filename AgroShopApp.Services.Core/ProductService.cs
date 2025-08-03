using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository;
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
        private readonly ICartRepository _cartRepository;

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IFavoriteRepository favoriteRepository,
            ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _favoriteRepository = favoriteRepository;
            _cartRepository = cartRepository;

        }

        public async Task<PaginatedProductListViewModel> GetPaginatedAsync(int page, int pageSize, int? categoryId = null, string? searchTerm = null, Guid? userId = null)
        {
            var products = await _productRepository.GetAllWithCategoryAsync();
            var filtered = products.Where(p => p.IsAvailable && !p.IsDeleted);

            if (categoryId.HasValue)
                filtered = filtered.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                filtered = filtered.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));

            List<Guid> favorites = new();
            Cart? cart = null;

            if (userId is Guid uid)
            {
                favorites = (await _favoriteRepository.GetUserFavoritesAsync(uid))
                    .Select(f => f.ProductId)
                    .ToList();

                cart = await _cartRepository.GetOrCreateCartAsync(uid);
            }

            int total = filtered.Count();
            var paged = filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new PaginatedProductListViewModel
            {
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                SelectedCategoryId = categoryId,
                CurrentSearch = searchTerm,
                PageSize = pageSize,
                Categories = (await _categoryRepository.GetAllSortedAsync())
                    .Select(c => new ProductCategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList(),
                Products = paged.Select(p => new AllProductsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    IsFavorite = favorites.Contains(p.Id),
                    StockQuantity = p.StockQuantity,
                    QuantityInCart = cart?.Items.FirstOrDefault(ci => ci.ProductId == p.Id)?.Quantity ?? 0
                }).ToList()
            };

            return viewModel;
        }
        public async Task<IEnumerable<ProductCategoryViewModel>> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllSortedAsync();

            return categories.Select(c => new ProductCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<AllProductsViewModel?> GetDetailsAsync(Guid id, Guid? userId = null)
        {
            var product = await _productRepository.GetWithCategoryByIdAsync(id);

            if (product == null || !product.IsAvailable || product.IsDeleted)
                return null;

            bool isFavorite = false;

            if (userId is Guid uid)
            {
                isFavorite = await _favoriteRepository.ExistsAsync(uid, product.Id);
            }

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
        public async Task<bool> IsOutOfStockAsync(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product == null || product.StockQuantity == 0 || product.IsDeleted || !product.IsAvailable;
        }
        public async Task<PaginatedDeletedProductListViewModel> GetDeletedPaginatedAsync(int page, int pageSize, int? categoryId, string? searchTerm)
        {
            var query = _productRepository.GetDeletedAttached();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm));

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderByDescending(p => p.DeletedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedDeletedProductListViewModel
            {
                DeletedProducts = products.Select(p => new DeletedProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    DeletedOn = p.DeletedOn
                }).ToList(),

                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                PageSize = pageSize,
                SelectedCategoryId = categoryId,
                CurrentSearch = searchTerm
            };
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }
    }
}

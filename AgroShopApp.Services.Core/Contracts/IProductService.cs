using AgroShopApp.Data.Models;
using AgroShopApp.Web.ViewModels.Product;
using System.Threading.Tasks;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface IProductService
    {
        Task<PaginatedProductListViewModel> GetPaginatedAsync(int page, int pageSize, int? categoryId = null, string? searchTerm = null, Guid? userId = null);
        Task<IEnumerable<ProductCategoryViewModel>> GetCategoriesAsync();
        Task<AllProductsViewModel?> GetDetailsAsync(Guid id, Guid? userId);
        Task CreateAsync(ProductFormViewModel model);
        Task RemoveAsync(Guid id);
        Task RestoreAsync(Guid id);
        Task<EditProductViewModel?> GetEditAsync(Guid id);
        Task EditAsync(EditProductViewModel model);
        Task<IEnumerable<DeletedProductViewModel>> GetDeletedDetailedAsync();
        Task<bool> IsOutOfStockAsync(Guid productId);
        Task<PaginatedDeletedProductListViewModel> GetDeletedPaginatedAsync(int page, int pageSize, int? categoryId, string? searchTerm);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}

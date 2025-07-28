using AgroShopApp.Web.ViewModels.Product;

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
    }
}

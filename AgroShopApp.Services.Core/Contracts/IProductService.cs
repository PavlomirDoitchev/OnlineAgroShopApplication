using AgroShopApp.Web.ViewModels.Product;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<AllProductsViewModel>> GetAllAsync(int? categoryId = null, string? searchTerm = null, string? userId = null);
        Task<IEnumerable<ProductCategoryViewModel>> GetCategoriesAsync();
        Task<AllProductsViewModel?> GetDetailsAsync(Guid id, string? userId);
        Task CreateAsync(ProductFormViewModel model);
        Task RemoveAsync(Guid id);
        Task<EditProductViewModel?> GetEditAsync(Guid id);
        Task EditAsync(EditProductViewModel model);
        Task<IEnumerable<AllProductsViewModel>> GetDeletedAsync();
    }
}

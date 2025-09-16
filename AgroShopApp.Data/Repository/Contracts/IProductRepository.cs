using AgroShopApp.Data.Models;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface IProductRepository : IAsyncRepository<Product, Guid>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product?> GetWithCategoryByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetDeletedProductsAsync();
        Task<Product?> GetByIdIncludingDeletedAsync(Guid id);
        IQueryable<Product> GetAllAttached();
        IQueryable<Product> GetDeletedAttached();
        IQueryable<Product> QueryAllWithCategory(bool includeDeleted);
        IQueryable<Product> QueryWithCategory();
    }
}
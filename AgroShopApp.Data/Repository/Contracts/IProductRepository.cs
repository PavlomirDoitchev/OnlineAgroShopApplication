using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface IProductRepository : IAsyncRepository<Product, Guid>
    {
        Task<Product?> GetWithCategoryAsync(Guid id);
    }
}
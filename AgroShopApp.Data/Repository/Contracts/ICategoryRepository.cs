using AgroShopApp.Data.Models;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface ICategoryRepository : IAsyncRepository<Category, int>
    {
        Task<IEnumerable<Category>> GetAllSortedAsync();
    }
}

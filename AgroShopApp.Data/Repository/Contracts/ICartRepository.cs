using AgroShopApp.Data.Models;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface ICartRepository : IRepository<Cart, Guid>, IAsyncRepository<Cart, Guid>
    {
        Task<Cart> GetOrCreateCartAsync(string userId);
        Task<Cart> GetWithItemsAsync(string userId);
    }
}
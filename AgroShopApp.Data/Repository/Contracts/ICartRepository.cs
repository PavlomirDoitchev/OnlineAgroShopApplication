using AgroShopApp.Data.Models;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface ICartRepository : IRepository<Cart, Guid>, IAsyncRepository<Cart, Guid>
    {
        Task<Cart> GetOrCreateCartAsync(Guid userId);
        Task<Cart> GetWithItemsAsync(Guid userId);
    }
}
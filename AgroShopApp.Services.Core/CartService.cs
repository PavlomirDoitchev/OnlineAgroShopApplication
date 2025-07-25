
using AgroShopApp.Services.Core.Contracts;

namespace AgroShopApp.Services.Core
{
    public class CartService : ICartService
    {
        public Task AddToCartAsync(string userId, Guid productId)
        {
            return Task.CompletedTask;
        }
    }
}

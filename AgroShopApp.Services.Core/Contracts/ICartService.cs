namespace AgroShopApp.Services.Core.Contracts
{
    public interface ICartService
    {
        Task AddToCartAsync(string userId, Guid productId);
    }
}
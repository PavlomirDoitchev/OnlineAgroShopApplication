using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Repository
{
    public class CartRepository : BaseRepository<Cart, Guid>, ICartRepository
    {
        public CartRepository(AgroShopDbContext context)
            : base(context)
        {
        }

        public async Task<Cart> GetOrCreateCartAsync(Guid userId)
        {
            var cart = await DbSet
        .Include(c => c.Items)
        .ThenInclude(i => i.Product)
        .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await DbSet.AddAsync(cart);
                await DbContext.SaveChangesAsync();
            }

            return cart;
        }

        public async Task<Cart> GetWithItemsAsync(Guid userId)
        {
            return await DbSet
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId)
                ?? throw new InvalidOperationException("Cart not found.");
        }
    }
}

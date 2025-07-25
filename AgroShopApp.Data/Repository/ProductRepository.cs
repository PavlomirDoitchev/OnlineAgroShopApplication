using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Repository
{
    public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
    {
        public ProductRepository(AgroShopDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Product?> GetWithCategoryAsync(Guid id)
        {
            return await DbSet
                .Include(p => p.Category)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

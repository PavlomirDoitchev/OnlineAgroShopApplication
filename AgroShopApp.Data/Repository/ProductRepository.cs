using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgroShopApp.Data.Repository
{
    public class ProductRepository : BaseRepository<Product, Guid>, IProductRepository
    {
        public ProductRepository(AgroShopDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Product?> GetWithCategoryByIdAsync(Guid id)
        {
            return await DbSet
                .Include(p => p.Category)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public IQueryable<Product> QueryAllWithCategory(bool includeDeleted = false)
        {
            var query = DbSet.Include(p => p.Category);
            if (includeDeleted)
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Product, Category>)query.IgnoreQueryFilters();
            return query;
        }
        public IQueryable<Product> QueryWithCategory()
        {
            return DbSet
                .Include(p => p.Category);
        }

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        {
            return await DbSet
                .Include(p => p.Category)
                .IgnoreQueryFilters()
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetDeletedProductsAsync()
        {
            return await DbSet
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted)
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<Product?> GetByIdIncludingDeletedAsync(Guid id)
        {
            return await DbSet
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public IQueryable<Product> GetDeletedAttached()
        {
            return DbSet
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted)
                .Include(p => p.Category);

        }
    }
}

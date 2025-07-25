using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Repository
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AgroShopDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllSortedAsync()
        {
            return await DbSet
                .OrderBy(c => c.Name)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

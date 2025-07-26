using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Data.Repository
{
    public class OrderRepository : BaseRepository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(AgroShopDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllByUserAsync(string userId)
        {
            return await DbSet
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderedOn)
                .ToListAsync();
        }

        public async Task<Order?> GetWithItemsAsync(Guid orderId)
        {
            return await DbSet
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}

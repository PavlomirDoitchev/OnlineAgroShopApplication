using AgroShopApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Data.Repository.Contracts
{
    public interface IOrderRepository : IRepository<Order, Guid>, IAsyncRepository<Order, Guid>
    {
        Task<IEnumerable<Order>> GetAllByUserAsync(string userId);
        Task<Order?> GetWithItemsAsync(Guid orderId);
    }
}

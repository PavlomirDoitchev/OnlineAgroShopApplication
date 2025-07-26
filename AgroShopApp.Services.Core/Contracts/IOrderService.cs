using AgroShopApp.Web.ViewModels.Order;
using AgroShopApp.Web.ViewModels.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(string userId);
        Task<PaginatedOrderListViewModel> GetPaginatedUserOrdersAsync(string userId, int page, int pageSize);
        Task<OrderDetailsViewModel?> GetDetailsAsync(Guid orderId, string userId);
    }
}

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
        Task PlaceOrderAsync(Guid userId);
        Task<PaginatedOrderListViewModel> GetPaginatedUserOrdersAsync(Guid userId, int page, int pageSize);
        Task<OrderDetailsViewModel?> GetDetailsAsync(Guid orderId, Guid userId);
    }
}

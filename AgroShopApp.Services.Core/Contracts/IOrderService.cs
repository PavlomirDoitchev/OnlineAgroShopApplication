using AgroShopApp.Web.ViewModels.Order;
using AgroShopApp.Web.ViewModels;

namespace AgroShopApp.Services.Core.Contracts
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(Guid userId);
        Task<PaginatedOrderListViewModel> GetPaginatedUserOrdersAsync(Guid userId, int page, int pageSize);
        Task<OrderDetailsViewModel?> GetDetailsAsync(Guid orderId, Guid userId);
        Task<IEnumerable<AdminOrderListItemViewModel>> GetFilteredOrdersAsync(OrderFilterInputModel filter);
        Task<AdminOrderDetailsViewModel?> GetOrderDetailsAsync(Guid orderId);
        Task<PaginatedAdminOrderListViewModel> GetPaginatedFilteredOrdersAsync(OrderFilterInputModel filter, int page, int pageSize);
    }
}

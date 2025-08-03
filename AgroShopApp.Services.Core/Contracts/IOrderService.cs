using AgroShopApp.Web.ViewModels.Order;
using AgroShopApp.Web.ViewModels;
using System.Security.Claims;

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
        Task<bool> UpdateStatusAsync(Guid orderId, string newStatus);
        Task<List<OrderChartPoint>> GetOrderStatsAsync(DateTime from, DateTime to);
        Task<List<ProductSalesPoint>> GetTopSellingProductsAsync(int topN);
        Task<bool> TryCancelOrderAsync(Guid orderId, ClaimsPrincipal user);
    }
}



namespace AgroShopApp.Web.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int OrdersToday { get; set; }
        public int TotalUsers { get; set; }
        public int OutOfStock { get; set; }

        public List<AdminOrderListItemViewModel> TodaysOrders { get; set; } = new();
        public List<OrdersPerDayViewModel> OrdersLast7Days { get; set; } = new();
        public List<OrderChartPoint> RevenueLast7Days { get; set; } = new();
        public List<ProductSalesPoint> TopSellingProducts { get; set; } = new();
    }
}

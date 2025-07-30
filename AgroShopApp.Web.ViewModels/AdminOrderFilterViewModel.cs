using AgroShopApp.Web.ViewModels.Order;

namespace AgroShopApp.Web.ViewModels
{
    public class AdminOrderFilterViewModel
    {
        public string? Status { get; set; }
        public string? UserEmail { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string? OrderId { get; set; }

        public List<OrderSummaryViewModel> Orders { get; set; } = new();
    }
}

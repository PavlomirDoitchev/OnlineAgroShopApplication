using AgroShopApp.Web.ViewModels.Order;

namespace AgroShopApp.Web.ViewModels
{
    public class AdminOrderDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public DateTime OrderedOn { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }

        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}

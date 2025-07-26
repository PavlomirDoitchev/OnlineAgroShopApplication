namespace AgroShopApp.Web.ViewModels.Cart
{
    public class ConfirmOrderViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}

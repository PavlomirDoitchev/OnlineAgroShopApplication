namespace AgroShopApp.Web.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int StockQuantity { get; set; }
    }
}
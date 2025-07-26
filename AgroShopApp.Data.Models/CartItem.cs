namespace AgroShopApp.Data.Models
{
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; } = null!;

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}

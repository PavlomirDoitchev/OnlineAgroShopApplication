namespace AgroShopApp.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime? DeletedOn { get; set; } = null;
        public int StockQuantity { get; set; } = 10;
        public bool IsAvailable { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

    }
}

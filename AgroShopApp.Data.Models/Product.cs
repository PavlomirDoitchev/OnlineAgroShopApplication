using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Models
{
    [Comment("Product available for purchase")]
    public class Product
    {
        [Comment("Product identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Product name")]
        public string Name { get; set; } = null!;

        [Comment("Detailed description of the product")]
        public string Description { get; set; } = null!;

        [Comment("Price per unit")]
        public decimal Price { get; set; }

        [Comment("Optional product image URL")]
        public string? ImageUrl { get; set; }

        [Comment("Date product was added to the store")]
        public DateTime AddedOn { get; set; }

        [Comment("Soft-delete timestamp")]
        public DateTime? DeletedOn { get; set; } = null;

        [Comment("Available stock quantity")]
        public int StockQuantity { get; set; } = 10;

        [Comment("Whether product is marked as currently available")]
        public bool IsAvailable { get; set; } = true;

        [Comment("Whether product is soft-deleted")]
        public bool IsDeleted { get; set; } = false;

        [Comment("Foreign key to category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    }
}

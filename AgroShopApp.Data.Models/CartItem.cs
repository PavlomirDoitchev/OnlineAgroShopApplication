using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Models
{
    [Comment("Product-to-cart mapping with quantity")]
    public class CartItem
    {
        [Comment("Cart identifier (composite key)")]
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; } = null!;

        [Comment("Product added to the cart (composite key)")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        [Comment("Number of items added")]
        public int Quantity { get; set; }
    }
}

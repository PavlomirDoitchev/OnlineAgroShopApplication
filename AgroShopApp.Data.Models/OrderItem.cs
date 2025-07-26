using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Models
{
    [Comment("Mapping between product and order with quantity and price snapshot")]
    public class OrderItem
    {
        [Comment("Parent order ID (composite key)")]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        [Comment("Ordered product ID (composite key)")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        [Comment("Quantity of product in the order")]
        public int Quantity { get; set; }

        [Comment("Unit price of product at the time of purchase")]
        public decimal UnitPrice { get; set; }
    }
}

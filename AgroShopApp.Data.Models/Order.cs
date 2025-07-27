using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AgroShopApp.Data.Models
{
    [Comment("Submitted order with snapshot data")]
    public class Order
    {
        [Comment("Order identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("User who placed the order")]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        [Comment("Date and time when the order was placed")]
        public DateTime OrderedOn { get; set; } = DateTime.Now;

        [Comment("Current status of the order")]
        public string Status { get; set; } = "Pending";

        [Comment("Order items associated with this order")]
        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        [Comment("Total amount of the order at the time of purchase")]
        public decimal TotalAmount { get; set; }
    }
}

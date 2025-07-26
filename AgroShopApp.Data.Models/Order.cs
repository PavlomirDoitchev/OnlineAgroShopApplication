using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgroShopApp.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Pending"; // optional enum later

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal TotalAmount { get; set; } // snapshot
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Models
{
    [Comment("User's Cart in the system")]
    public class Cart
    {
        [Comment("Cart identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("User who owns the cart")]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        [Comment("Products added to the cart")]
        public virtual ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();
    }
}
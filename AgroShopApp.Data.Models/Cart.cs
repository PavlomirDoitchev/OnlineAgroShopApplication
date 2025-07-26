using Microsoft.AspNetCore.Identity;

namespace AgroShopApp.Data.Models
{
    public class Cart
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        public virtual ICollection<CartItem> Items { get; set; } = new HashSet<CartItem>();
    }
}
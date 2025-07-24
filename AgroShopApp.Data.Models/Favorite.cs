using Microsoft.AspNetCore.Identity;

namespace AgroShopApp.Data.Models
{
    public class Favorite
    {
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
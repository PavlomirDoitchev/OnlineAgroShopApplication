using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Models
{
    [Comment("User-product favorites")]
    public class Favorite
    {
        [Comment("User who favorited the product")]
        public string UserId { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;

        [Comment("Favorited product ID")]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;

        [Comment("Timestamp when product was added to favorites")]
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
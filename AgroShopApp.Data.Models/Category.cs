using Microsoft.EntityFrameworkCore;

namespace AgroShopApp.Data.Models
{
    [Comment("Product category in the catalog")]
    public class Category
    {
        [Comment("Category identifier")]
        public int Id { get; set; }

        [Comment("Category name")]
        public string Name { get; set; } = null!;

        [Comment("Products assigned to this category")]
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
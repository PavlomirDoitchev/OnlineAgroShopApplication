using System.ComponentModel.DataAnnotations;

namespace AgroShopApp.Web.ViewModels.Product
{
    public class ProductFormViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Stock Quantity")]
        [Range(1, 10000)]
        public int StockQuantity { get; set; } = 10;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; } = new List<ProductCategoryViewModel>();
    }
}

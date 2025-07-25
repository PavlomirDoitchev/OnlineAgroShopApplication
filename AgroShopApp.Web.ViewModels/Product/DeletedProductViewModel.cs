namespace AgroShopApp.Web.ViewModels.Product
{
    public class DeletedProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public string Category { get; set; } = null!;

        public DateTime? DeletedOn { get; set; }
    }
}
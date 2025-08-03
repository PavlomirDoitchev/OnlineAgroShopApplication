namespace AgroShopApp.Web.ViewModels.Product
{
    public class PaginatedDeletedProductListViewModel
    {
        public List<DeletedProductViewModel> DeletedProducts { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int? SelectedCategoryId { get; set; }

        public string? CurrentSearch { get; set; }

        public List<ProductCategoryViewModel> Categories { get; set; } = new();
    }
}
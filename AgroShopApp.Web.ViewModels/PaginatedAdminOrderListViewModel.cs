namespace AgroShopApp.Web.ViewModels
{
    public class PaginatedAdminOrderListViewModel
    {
        public List<AdminOrderListItemViewModel> Orders { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public OrderFilterInputModel Filter { get; set; } = new();
    }
}

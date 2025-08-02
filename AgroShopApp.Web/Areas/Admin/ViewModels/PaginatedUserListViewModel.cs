

namespace AgroShopApp.Web.ViewModels
{
    public class PaginatedUserListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }

}

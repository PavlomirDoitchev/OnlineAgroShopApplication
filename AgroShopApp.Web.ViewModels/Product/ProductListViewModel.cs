using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Web.ViewModels.Product
{
    public class ProductListViewModel
    {
        public IEnumerable<AllProductsViewModel> Products { get; set; } = new List<AllProductsViewModel>();
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public int? SelectedCategoryId { get; set; }
    }
}
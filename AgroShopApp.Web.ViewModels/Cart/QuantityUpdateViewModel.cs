using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Web.ViewModels.Cart
{
    public class QuantityUpdateViewModel
    {
        public Guid ProductId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
    }
}

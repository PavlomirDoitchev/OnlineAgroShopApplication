using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroShopApp.Web.ViewModels.Order
{
    public class OrderDetailsViewModel
    {
        public Guid Id { get; set; }
        public DateTime OrderedOn { get; set; }
        public string Status { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; } = null!;
        public List<OrderItemViewModel> Items { get; set; } = new();
    }
}

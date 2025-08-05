using System.ComponentModel.DataAnnotations;

namespace AgroShopApp.Web.ViewModels
{
    public class OrderFilterInputModel
    {
        public string? Email { get; set; }
        public string? Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }
    }
}

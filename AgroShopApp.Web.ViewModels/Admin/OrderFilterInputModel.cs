namespace AgroShopApp.Web.ViewModels
{
    public class OrderFilterInputModel
    {
        public string? Email { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AgroShopApp.Web.ViewModels.Cart
{
    public class ConfirmOrderViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        [Required(ErrorMessage = "Please select a ciy")]
        public string SelectedCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a street address.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Street address must be between 5 and 100 characters.")]
        public string StreetAddress { get; set; } = string.Empty;
        public string DeliveryAddress => $"{StreetAddress}, {SelectedCity}";
    }
}

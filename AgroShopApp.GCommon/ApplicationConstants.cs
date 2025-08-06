namespace AgroShopApp.GCommon
{
    public static class ApplicationConstants
    {
        public const string HoursFormat = "HH:mm";
        public const string MonthDayFormat = "MMM dd";
        public const string AppDateFormat = "yyyy-MM-dd";
        public const string AppDateFormatHHMM = "yyyy-MM-dd HH:mm";
        public const string IsDeletedPropertyName = "IsDeleted";
        public const string PriceSqlType = "decimal(18, 6)";
        public const string AccessDeniedPath = "/Home/AccessDenied";
        public const string ManagerAuthCookie = "ManagerAuth";
        public const string AllowAllDomainsPolicy = "AllowAllDomainsDebug";
        public const int TruncateDescriptionLength = 85;
        public static string ToCurrency(decimal value)
          => value.ToString("C");

        public static class TempDataMessages
        {
            public const string ProductAddedToCart = "Product added to cart.";
            public const string ProductQuantityUpdated = "Product quantity updated.";
            public const string ProductRemovedFromCart = "Product removed from cart.";
            public const string ProductNotFound = "Product not found.";
            public const string ProductAlreadyInCart = "Product is already in your cart.";
            public const string ProductAddedToFavorites = "Product added to favorites.";
            public const string ProductRemovedFromFavorites = "Product removed from favorites.";
            public const string ProductCreated = "Product created successfully.";
            public const string ProductUpdated = "Product updated successfully.";
            public const string ProductDeleted = "Product deleted successfully.";
            public const string ProductRestored = "Product restored successfully.";

            public const string CartIsEmpty = "Your cart is empty.";

            public const string OrderPlaced = "Order placed successfully.";
            public const string OrderCancelled = "Order cancelled successfully.";
            public const string OrderUpdated = "Order updated successfully.";
            public const string OrderNotFound = "Order not found.";
            public const string OrderCouldNotBeCancelled = "Order could not be cancelled.";
            public const string OrderAlreadyCancelled = "Order is already cancelled.";

            public const string OrderInvalidStatus = "Invalid order status selected.";
            public const string OrderStatusUpdated = "Order status updated successfully.";
            public const string OrderDetailsNotFound = "Order details not found.";
            public const string OrderStatusChangeNotAllowed = "Status change not allowed. Completed or cancelled orders cannot be changed.";

            public const string CannotDeleteAdmin = "Cannot delete an admin account.";
            public const string UserDeleted = "User deleted successfully.";
            public const string UserRestored = "User restored successfully.";

        }
        public static class OrderStatuses
        {
            public const string Pending = "Pending";
            public const string Completed = "Completed";
            public const string Cancelled = "Cancelled";
        }
        public static class OrderStatusColors
        {
            public const string Pending = "warning";
            public const string Completed = "success";
            public const string Cancelled = "danger";
        }
    }
    public static class UserRoles
    {
        public const string AppAdmin = "Admin";
        public const string User = "User";
        public const string Guest = "Guest";
    }
    public static class AvailableCities
    {
        public static readonly string[] Cities = new[]
        {
            "Sofia",
            "Plovdiv",
            "Varna",
            "Burgas",
            "Ruse",
            "Stara Zagora",
            "Pleven",
            "Sliven",
            "Dobrich",
            "Shumen"
        };

    }
}

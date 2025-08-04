namespace AgroShopApp.GCommon
{
    public static class ApplicationConstants
    {
        public const string HoursFormat = "HH:mm";
        public const string AppDateFormat = "yyyy-MM-dd";
        public const string AppDateFormatHHMM = "yyyy-MM-dd HH:mm";
        public const string NoImageUrl = "no-image.jpg";
        public const string IsDeletedPropertyName = "IsDeleted";
        public const string PriceSqlType = "decimal(18, 6)";
        public const string AccessDeniedPath = "/Home/AccessDenied";
        public const string ManagerAuthCookie = "ManagerAuth";
        public const string AllowAllDomainsPolicy = "AllowAllDomainsDebug";
        public const int TruncateDescriptionLength = 85;
        public static string ToCurrency(decimal value)
          => value.ToString("C"); 
    }
   
}

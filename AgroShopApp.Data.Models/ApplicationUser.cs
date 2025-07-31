using Microsoft.AspNetCore.Identity;
using System;

namespace AgroShopApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
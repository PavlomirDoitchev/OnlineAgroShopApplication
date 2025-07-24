namespace AgroShopApp.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AgroShopDbContext : IdentityDbContext
    {
        public AgroShopDbContext(DbContextOptions<AgroShopDbContext> options)
            : base(options)
        {

        }
    }
}

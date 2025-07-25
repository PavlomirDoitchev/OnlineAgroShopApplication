using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AgroShopApp.Data;
namespace AspNetCoreArchTemplate.Web
{
    using AgroShopApp.Data.Repository.Contracts;
    using AgroShopApp.Data.Repository;
    using AgroShopApp.Services.Core;
    using AgroShopApp.Services.Core.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services
                .AddDbContext<AgroShopDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<AgroShopDbContext>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IFavoritesService, FavoritesService>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddControllersWithViews();

            WebApplication? app = builder.Build();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

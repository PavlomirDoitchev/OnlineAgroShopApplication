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
    using AgroShopApp.Web.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using AgroShopApp.Data.Models;
    using AgroShopApp.Data.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services
                .AddDbContext<AgroShopDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });


            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services
                    .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
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
                 .AddEntityFrameworkStores<AgroShopDbContext>()
                 .AddRoles<IdentityRole<Guid>>()
                 .AddSignInManager<SignInManager<ApplicationUser>>()
                 .AddUserManager<UserManager<ApplicationUser>>();

            builder.Services.AddRepositories(typeof(IProductRepository).Assembly);
            builder.Services.AddUserDefinedServices(typeof(IProductService).Assembly);

            builder.Services.AddHttpContextAccessor();


            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            WebApplication app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DatabaseSeeder.SeedRoles(services);
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}

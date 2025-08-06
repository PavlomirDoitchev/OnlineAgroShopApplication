using AgroShopApp.Data;
using AgroShopApp.Data.Configuration;
using AgroShopApp.Data.Models;
using AgroShopApp.Data.Repository;
using AgroShopApp.Data.Repository.Contracts;
using AgroShopApp.Services.Core;
using AgroShopApp.Services.Core.Contracts;
using AgroShopApp.Web.Infrastructure.Extensions;
using AgroShopApp.Web.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreArchTemplate.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AgroShopDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password = new PasswordOptions
                    {
                        RequiredLength = 3,
                        RequireNonAlphanumeric = false,
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                        RequiredUniqueChars = 0
                    };
                })
                .AddEntityFrameworkStores<AgroShopDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<SignInManager<ApplicationUser>, CustomSignInManager>();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Home/UnauthorizedError";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            builder.Services.AddRepositories(typeof(IProductRepository).Assembly);
            builder.Services.AddUserDefinedServices(typeof(IProductService).Assembly);
            builder.Services.AddHttpContextAccessor();

            //builder.Services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //});
            builder.Services.AddRazorPages();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                DatabaseSeeder.SeedRoles(services);
                DatabaseSeeder.AssignAdminRole(services);
                DatabaseSeeder.SeedRegularUsers(services);
                DatabaseSeeder.SeedOrders(services);
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

            app.Use(async (context, next) =>
            {
                if (context.User.Identity?.IsAuthenticated == true && context.Request.Path == "/")
                {
                    if (context.User.IsInRole("Admin"))
                    {
                        context.Response.Redirect("/Admin/Home/Index");
                        return;
                    }
                }

                await next();
            });

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

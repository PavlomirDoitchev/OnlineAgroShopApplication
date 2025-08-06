using AgroShopApp.Data;
using AgroShopApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class DatabaseSeeder
{
    public static void SeedRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            var roleExists = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
            if (!roleExists)
            {
                var result = roleManager.CreateAsync(new IdentityRole<Guid> { Name = role }).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role: {role}");
                }
            }
        }
    }

    public static void AssignAdminRole(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string adminEmail = "admin@example.com";
        string adminPassword = "Admin@123";
        string adminFirstName = "Admin";
        string adminLastName = "User";
        var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = adminFirstName,
                LastName = adminLastName,
                IsDeleted = false
            };
            var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
            if (!createUserResult.Succeeded)
            {
                throw new Exception($"Failed to create admin user: {adminEmail}");
            }
        }

        var isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
        if (!isInRole)
        {
            var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            if (!addRoleResult.Succeeded)
            {
                throw new Exception($"Failed to assign admin role to user: {adminEmail}");
            }
        }
    }
    public static void SeedRegularUsers(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        for (int i = 1; i <= 50; i++)
        {
            string email = $"user{i}@example.com";
            string firstName = $"User{i}";
            string lastName = $"LastName{i}";

            var existingUser = userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (existingUser != null) continue;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                IsDeleted = false
            };

            var result = userManager.CreateAsync(user, "User@123").GetAwaiter().GetResult();
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create user: {email}");
            }

            var addToRoleResult = userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
            if (!addToRoleResult.Succeeded)
            {
                throw new Exception($"Failed to assign 'User' role to {email}");
            }
        }
    }
    public static void SeedOrders(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<AgroShopDbContext>();

        if (dbContext.Orders.Any()) return;

        var users = new List<ApplicationUser>();
        for (int i = 1; i <= 10; i++)
        {
            var email = $"user{i}@example.com";
            var user = userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user != null) users.Add(user);
        }

        var productIds = dbContext.Products.Select(p => p.Id).ToList();
        var random = new Random();
        var statuses = new[] { "Pending", "Completed", "Cancelled" };
        var streets = new[] { "Ivan Vazov St", "Tsarigradsko Shose Blvd", "Tsaritsa Yoanna Blvd", "Dimitar Petkov St", "Trayko Stanoev St", "Stoyan Popov St" };
        var cities = new[] { "Sofia", "Plovdiv", "Varna", "Burgas", "Stara Zagora", "Shumen" };

        var orders = new List<Order>();

        for (int i = 0; i < 30; i++) 
        {
            var user = users[random.Next(users.Count)];
            var orderDate = DateTime.Now.Date.AddDays(-random.Next(0, 15)); 
            var status = statuses[random.Next(statuses.Length)];
            var street = streets[random.Next(streets.Length)];
            var city = cities[random.Next(cities.Length)];
            var address = $"{random.Next(1, 140)} {street}, {city}";

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                OrderedOn = orderDate,
                Status = status,
                DeliveryAddress = address,
                Items = new List<OrderItem>()
            };

            decimal total = 0;

            var selectedProductIds = productIds.OrderBy(_ => random.Next()).Take(random.Next(2, 5)).ToList();
            foreach (var productId in selectedProductIds)
            {
                var product = dbContext.Products.First(p => p.Id == productId);
                var quantity = random.Next(1, 5);
                var unitPrice = product.Price;

                order.Items.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });

                total += quantity * unitPrice;
            }

            order.TotalAmount = Math.Round(total, 2);
            orders.Add(order);
        }

        dbContext.Orders.AddRange(orders);
        dbContext.SaveChanges();
    }
}

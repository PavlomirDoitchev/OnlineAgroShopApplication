﻿using AgroShopApp.Data.Models;
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
}

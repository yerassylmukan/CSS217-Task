using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data;

public static class AppIdentitySeedData
{
    public static async Task SeedData(AppIdentityDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (dbContext.Database.IsNpgsql()) dbContext.Database.Migrate();

        await EnsureRoleExistsAsync(roleManager, "Admin");
        await EnsureRoleExistsAsync(roleManager, "User");

        var adminUserName = "admin@gmail.com";
        var adminUser = new ApplicationUser
        {
            UserName = adminUserName,
            Email = adminUserName
        };

        if (await userManager.FindByEmailAsync(adminUserName) == null)
        {
            await userManager.CreateAsync(adminUser, "Admin-1234");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        var user1UserName = "user1@gmail.com";
        var user1 = new ApplicationUser
        {
            UserName = user1UserName,
            Email = user1UserName
        };

        if (await userManager.FindByEmailAsync(user1UserName) == null)
        {
            await userManager.CreateAsync(user1, "User1-1234");
            await userManager.AddToRoleAsync(user1, "User");
        }

        var user2UserName = "user2@gmail.com";
        var user2 = new ApplicationUser
        {
            UserName = user2UserName,
            Email = user2UserName
        };

        if (await userManager.FindByEmailAsync(user2UserName) == null)
        {
            await userManager.CreateAsync(user2, "User2-1234");
            await userManager.AddToRoleAsync(user2, "User");
        }
    }

    private static async Task EnsureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName)) await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}
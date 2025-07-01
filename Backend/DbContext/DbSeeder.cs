using FitSync.Entities;
using FitSync.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FitSync.DbContext;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await ctx.Database.MigrateAsync();

        /* ---------- 1) Kreiraj role ako ne postoje ---------- */
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleMgr.RoleExistsAsync(role))
                await roleMgr.CreateAsync(new IdentityRole(role));
        }

        /* ---------- 2) Seed admin ---------- */
        await EnsureUserWithRoleAsync(userMgr, "d6f53391-3e40-4e9d-9c1d-7c1b91faadfb",
            username: "admin",
            email: "admin@fitsync.local",
            password: "Admin123!",
            role: "Admin");

        /* ---------- 3) Seed milan ---------- */
        await EnsureUserWithRoleAsync(userMgr, "fe7dd50c-8b0e-42f0-bfed-0b52e08fe8c9",
            username: "milan",
            email: "milan@fitsync.local",
            password: "Milan123!",
            role: "User");
    }

    private static async Task EnsureUserWithRoleAsync(
        UserManager<User> userMgr,
        string id,
        string username,
        string email,
        string password,
        string role)
    {
        var user = await userMgr.FindByNameAsync(username);
        if (user != null) return;

        user = new User
        {
            Id = id,
            UserName = username,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userMgr.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new Exception($"❌ Cannot create user {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        await userMgr.AddToRoleAsync(user, role);
    }
}

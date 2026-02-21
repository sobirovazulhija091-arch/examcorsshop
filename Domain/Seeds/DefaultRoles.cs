using System;
using Microsoft.AspNetCore.Identity;

public class DefaultRoles
{
    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "Manager", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    internal static async Task SeedRoles(object role)
    {
        throw new NotImplementedException();
    }
}
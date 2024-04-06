﻿using Microsoft.AspNetCore.Identity;
using Project.Data.Models;
using static Project.Constants.RoleConstants;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task CreateRoleAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(AdminRole) == false)
            {
                var role = new IdentityRole(AdminRole);
                await roleManager.CreateAsync(role);

                var admin = await userManager.FindByEmailAsync("admin@gmail.com");

                if (admin != null)
                {
                    await userManager.AddToRoleAsync(admin, role.Name);
                }
            }

            if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(Project.Constants.RoleConstants.Restaurateur) == false)
            {
                var role = new IdentityRole(Project.Constants.RoleConstants.Restaurateur);
                await roleManager.CreateAsync(role);

                var restaurateur = await userManager.FindByEmailAsync("admin@gmail.com");

                if (restaurateur != null)
                {
                    await userManager.AddToRoleAsync(restaurateur, role.Name);
                }
            }

            if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(OwnerRole) == false)
            {
                var role = new IdentityRole(OwnerRole);
                await roleManager.CreateAsync(role);

                var restaurateur = await userManager.FindByEmailAsync("admin@gmail.com");

                if (restaurateur != null)
                {
                    await userManager.AddToRoleAsync(restaurateur, role.Name);
                }
            }
        }
    }
}

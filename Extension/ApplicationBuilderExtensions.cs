using Microsoft.AspNetCore.Identity;
using Project.Data.Models;
using System.Data;
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

            if (userManager != null && roleManager != null && await roleManager.RoleExistsAsync(Restaurateur) == false)
            {
                var role = new IdentityRole(Restaurateur);
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

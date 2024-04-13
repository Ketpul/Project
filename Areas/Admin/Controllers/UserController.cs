using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Admin.Models;
using Project.Data.Models;
using Project.Data.SeedDb;
using static Project.Constants.RoleConstants;

namespace Project.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext data;


        public UserController(UserManager<ApplicationUser> _userManager, ApplicationDbContext _data, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            data = _data;
            roleManager = _roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> AllUser()
        {
            var users = await userManager.Users.ToListAsync();

            var userViewInfoModels = new List<UserViewInfoModel>();

            foreach (var user in users)
            {
                var isAdmin = await userManager.IsInRoleAsync(user, AdminRole);
                var isRestaurateur = await userManager.IsInRoleAsync(user, Restaurateur);
                var isOwner = await userManager.IsInRoleAsync(user, OwnerRole);
                var isRestauranteur = await userManager.IsInRoleAsync(user, Restaurateur);

                userViewInfoModels.Add(new UserViewInfoModel
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsAdmin = isAdmin,
                    IsRestaurateur = isRestaurateur,
                    IsOwner = isOwner,
                    IsRestauranteur =isRestauranteur,
                });
            }

            return View(userViewInfoModels); 
        }

        [HttpGet]
        public async Task<IActionResult> AddAdmin(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            await userManager.AddToRoleAsync(user, AdminRole);

            data.SaveChanges();
            return RedirectToAction(nameof(AllUser));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromAdmin(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            await userManager.RemoveFromRoleAsync(user, AdminRole);

            data.SaveChanges();
            return RedirectToAction(nameof(AllUser));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromRestaurenteur(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            await userManager.RemoveFromRoleAsync(user, Restaurateur);

            data.SaveChanges();
            return RedirectToAction(nameof(AllUser));
        }
    }
}

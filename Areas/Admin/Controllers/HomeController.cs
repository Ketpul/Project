using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Admin.Models;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models.RestaurantViews;
using static Project.Constants.RoleConstants;

namespace Project.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext data;
        public HomeController(ApplicationDbContext _data, UserManager<ApplicationUser> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }

        public async Task<IActionResult> All()
        {
            var restaurateurRequest = await data.OwnersRequests
                .Select(r => new RestaurateurInfoViewModel()
                {
                    OwnerId = r.OwnerId,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    Information = r.Information,
                    PhoneNumber = r.PhoneNumber,
                    DateTime = r.DateTime,
                })
                .ToListAsync();

            return View(restaurateurRequest);
        }


        public async Task<IActionResult> Yes(string id)
        {

            var restaurateur = await userManager.FindByIdAsync(id);
            var ownersRequests = await data.OwnersRequests.FirstAsync(o => o.OwnerId == id);

            if (restaurateur != null)
            {
                await userManager.AddToRoleAsync(restaurateur, Restaurateur);

                
                data.OwnersRequests.Remove(ownersRequests);
                await data.SaveChangesAsync();
            }



            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> No(string id)
        {
            var ownersRequests = await data.OwnersRequests.FirstAsync(o => o.OwnerId == id);

            data.OwnersRequests.Remove(ownersRequests);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

    }
}

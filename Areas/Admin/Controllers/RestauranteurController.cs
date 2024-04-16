using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Admin.Models;
using Project.Data.Models;
using Project.Data.SeedDb;
using System.Data;
using System.Security.Claims;
using static Project.Constants.RoleConstants;

namespace Project.Areas.Admin.Controllers
{
    
    public class RestauranteurController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext data;
        public RestauranteurController(ApplicationDbContext _data, UserManager<ApplicationUser> _userManager)
        {
            data = _data;
            userManager = _userManager;
        }


        public async Task<IActionResult> AllRequest()
        {
            var restaurateurRequest = await data.RestaurateursRequests
                .Select(r => new RestaurateurInfoViewModel()
                {
                    RestaurateurId = r.RestaurateurId,
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
            var restaurateurRequests = await data.RestaurateursRequests.FirstAsync(o => o.RestaurateurId == id);

            if (restaurateur != null)
            {
                await userManager.AddToRoleAsync(restaurateur, Constants.RoleConstants.Restaurateur);


                restaurateur.FirstName = restaurateurRequests.FirstName;
                restaurateur.LastName = restaurateurRequests.LastName;
                restaurateur.PhoneNumber = restaurateurRequests.PhoneNumber;

                data.RestaurateursRequests.Remove(restaurateurRequests);
                await data.SaveChangesAsync();
            }



            return RedirectToAction(nameof(AllRequest)); 
        }

        public async Task<IActionResult> No(string id)
        {
            var RestaurateurRequests = await data.RestaurateursRequests.FirstAsync(o => o.RestaurateurId == id);

            data.RestaurateursRequests.Remove(RestaurateurRequests);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(AllRequest));
        }


        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}

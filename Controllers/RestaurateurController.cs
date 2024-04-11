using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models.OwnerViews;
using System.Security.Claims;

namespace Project.Controllers
{
    public class RestaurateurController : BaseController
    {
        private readonly ApplicationDbContext data;
        public RestaurateurController(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<IActionResult> Add()
        {
            var model = new RestaurateurRequestFromViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RestaurateurRequestFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();


            var restaurateur = new RestaurateurRequest()
            {
                RestaurateurId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Information = model.Information,
                DateTime =  DateTime.Now,
            };

            await data.RestaurateursRequests.AddAsync(restaurateur);
            await data.SaveChangesAsync();

            return RedirectToAction();

        }



        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models;
using System.Security.Claims;

namespace Project.Controllers
{
    public class OwnerController : Controller
    {
        private readonly ApplicationDbContext data;
        public OwnerController(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<IActionResult> Add()
        {
            var model = new OwnerRequestFromViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OwnerRequestFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();


            var owner = new OwnerRequest()
            {
                OwnerId = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Information = model.Information,
                DateTime =  DateTime.Now,
            };

            await data.OwnersRequests.AddAsync(owner);
            await data.SaveChangesAsync();

            return RedirectToAction();

        }



        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models.OtherViews;
using Project.Models.RestaurantViews;
using System.Security.Claims;

namespace Project.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext data;
        public RestaurantController(ApplicationDbContext _data)
        {
            data = _data;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new RestaurantFormViewModel();
            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RestaurantFormViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            string userId = GetUserId();

            var restaurant = new Restaurant()
            {
                Name = model.Name,
                ImageUrl=model.ImageUrl,
                Description = model.Description,
                CategoryId = model.CategoryId,
                OwnerId = userId,
                Address = model.Address,
            };


            Owner owner = await data.Owners.FirstAsync(x => x.UserId == GetUserId());
            owner.Restaurants.Add(restaurant);

            await data.Restaurants.AddAsync(restaurant);
            await data.SaveChangesAsync();

            return RedirectToAction();
        }

        public async Task<IActionResult> All()
        {
            

            var restaurants = await data.Restaurants
                .Select(r => new RestaurantInfoViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    ImageUrl = r.ImageUrl,
                    Description = r.Description,
                    Address = r.Address,
                    Owner = r.OwnerId,
                    Category = r.Category.Name
                })
                .ToListAsync();

            return View(restaurants);
        }


        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        
    }
}

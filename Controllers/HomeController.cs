using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.SeedDb;
using Project.Models.OtherViews;
using System.Diagnostics;

namespace Project.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext data;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _data)
        {
            _logger = logger;
            data = _data;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = await data.Restaurants.OrderByDescending(r => r.AvgRating).Take(3).ToListAsync();

            var model = new HomeIndexViewModel()
            {
                Restaurants = restaurants
            };

            return View(model);
        }


        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400)
            {
                return View("Error400");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }
            
            if (statusCode == 404)
            {
                return View("Error404");
            }

            return View();
        }

    }
}

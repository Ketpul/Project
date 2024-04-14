using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models.OtherViews;
using Project.Models.RestaurantViews;
using System.Security.Claims;
using static Project.Constants.MessageConstants;
using static Project.Constants.RoleConstants;

namespace Project.Controllers
{
    public class RestaurantController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> users;
        public RestaurantController(ApplicationDbContext _data, UserManager<ApplicationUser> _users)
        {
            data = _data;
            users = _users;
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
                ImageUrl1 = model.ImageUrl1,
                ImageUrl2 = model.ImageUrl2,
                ImageUrl3 = model.ImageUrl3,
                Description = model.Description,
                CategoryId = model.CategoryId,
                RestaurateurId = userId,
                Address = model.Address,
                GoogleMaps = model.GoogleMaps,
                RegionalCity = model.RegionalCity,
            };

            await data.Restaurants.AddAsync(restaurant);
            await data.SaveChangesAsync();

            TempData[UserMessageSuccess] = "You added restaurants successfuly!";

            return RedirectToAction();
        }
        [HttpGet]
        public async Task<IActionResult> All(string searchString, string category, string city, int rating)
        {
            var restaurantsQuery = data.Restaurants.Select(r => new RestaurantInfoViewModel()
            {
                Id = r.Id,
                Name = r.Name,
                ImageUrl1 = r.ImageUrl1,
                ImageUrl2 = r.ImageUrl2,
                ImageUrl3 = r.ImageUrl3,
                Description = r.Description,
                Address = r.Address,
                RestaurateurId = r.RestaurateurId,
                Category = r.Category.Name,
                City = r.RegionalCity.ToString(),
                Rating = r.AvgRating,
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                restaurantsQuery = restaurantsQuery.Where(r => r.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(category))
            {
                restaurantsQuery = restaurantsQuery.Where(r => r.Category == category);
            }
            
            if (rating != 0)
            {
                restaurantsQuery = restaurantsQuery.Where(r => r.Rating == rating);
            }
            
            var restaurant = await restaurantsQuery.ToListAsync();

            if(!string.IsNullOrEmpty(city))
            {
                restaurant = restaurant.Where(r => r.City.ToString() == city).ToList();
            }


            

            return View(restaurant);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var restaurant = await data.Restaurants.FirstAsync(r => r.Id == id);
            var restaurantuer = await users.Users.FirstAsync(u => u.Id == restaurant.RestaurateurId);
            var categery = await data.Categories.FirstAsync(c => c.Id == restaurant.CategoryId);
            var comments = await data.Comments.Where(c => c.RestaurantId == restaurant.Id).ToListAsync();

            var model = new RestaurantDetailsViewModel()
            {
                Id=id,
                Name = restaurant.Name,
                ImageUrl = { restaurant.ImageUrl1, restaurant.ImageUrl2, restaurant.ImageUrl3 },
                Address = restaurant.Address,
                Description = restaurant.Description,
                RestaurateurId = restaurant.RestaurateurId,
                Category = categery.Name,
                FullName = $"{restaurantuer.FirstName} {restaurantuer.LastName}",
                Email = restaurantuer.Email,
                PhoneNumber = restaurantuer.PhoneNumber,
                Comments = comments,
                GoogleMaps = restaurant.GoogleMaps,
                UserName = restaurantuer.UserName,
                City = restaurant.RegionalCity.ToString(),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await data.Restaurants.FirstAsync(r => r.Id == id);

            if (!User.IsInRole(AdminRole))
            {
                if (restaurant.RestaurateurId != GetUserId())
                {
                    return Unauthorized();
                }
            }

            var model = new RestaurantFormViewModel()
            {
                Name = restaurant.Name,
                ImageUrl1 = restaurant.ImageUrl1,
                ImageUrl2 = restaurant.ImageUrl2,
                ImageUrl3 = restaurant.ImageUrl3,
                Address = restaurant.Address,
                Description = restaurant.Description,
                GoogleMaps = restaurant.GoogleMaps,
                Restaurateur = restaurant.RestaurateurId,
                RegionalCity = restaurant.RegionalCity,

            };

            model.Categories = await GetCategories();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RestaurantFormViewModel model, int id)
        {
            var restaurant = await data.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return BadRequest();
            }

            if (!User.IsInRole(AdminRole))
            {
                if (restaurant.RestaurateurId != GetUserId())
                {
                    return Unauthorized();
                }
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();

                return View(model);
            }

            var category = await data.Categories.FindAsync(model.CategoryId);

            restaurant.Name = model.Name;
            restaurant.ImageUrl1 = model.ImageUrl1;
            restaurant.ImageUrl2 = model.ImageUrl2;
            restaurant.ImageUrl3 = model.ImageUrl3;
            restaurant.Address = model.Address;
            restaurant.Description = model.Description;
            restaurant.GoogleMaps = model.GoogleMaps;
            restaurant.Category = category;
            restaurant.RegionalCity = model.RegionalCity; 

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await data.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return BadRequest();
            }

            if (!User.IsInRole(AdminRole))
            {
                if (restaurant.RestaurateurId != GetUserId())
                {
                    return Unauthorized();
                }
            }
            

            data.Remove(restaurant);
            await data.SaveChangesAsync();

            TempData[UserMessageError] = "You delete restaurants successfuly!";

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteRestaurants(int id)
        {
            string userId = GetUserId();

            var restaurants = await data.FavoritesRestaurants
                .Where(f => f.UserId == userId)
                .Select(r => new RestaurantInfoViewModel()
                {
                    Id = r.Restaurant.Id,
                    Name = r.Restaurant.Name,
                    ImageUrl1 = r.Restaurant.ImageUrl1,
                    ImageUrl2 = r.Restaurant.ImageUrl2,
                    ImageUrl3 = r.Restaurant.ImageUrl3,
                    Description = r.Restaurant.Description,
                    Address = r.Restaurant  .Address,
                    RestaurateurId = r.Restaurant.RestaurateurId,
                    Category = r.Restaurant.Category.Name
                })
                .ToListAsync();

            return View(restaurants);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavoriteRestaurants(int id)
        {
            var restaurant = await data.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var entity = new FavoriteRestaurants()
            {
                UserId = userId,
                RestaurantId = restaurant.Id
            };


            if (await data.FavoritesRestaurants.ContainsAsync(entity))
            {
                return RedirectToAction(nameof(FavoriteRestaurants));
            }

            await data.FavoritesRestaurants.AddAsync(entity);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(FavoriteRestaurants));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorite(int id)
        {
            var userId = GetUserId();
            var favorite = await data.FavoritesRestaurants.FirstAsync(f => f.RestaurantId == id && f.UserId == userId);

            if (favorite == null)
            {
                return BadRequest();
            }

            data.FavoritesRestaurants.Remove(favorite);
            data.SaveChanges();

            return RedirectToAction(nameof(FavoriteRestaurants));
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

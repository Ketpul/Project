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

        public async Task<IActionResult> AllUsers()
        {
            var users = await userManager.Users.ToListAsync();

            var employeeViewInfoModels = new List<EmployeeInfoViewModel>();

            foreach (var user in users)
            {
                var isEmployee = await userManager.IsInRoleAsync(user, Employee);
                var IsRestauntior = await userManager.IsInRoleAsync(user, Restaurateur);


                employeeViewInfoModels.Add(new EmployeeInfoViewModel
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsEmployee = isEmployee,
                    IsRestauntior = IsRestauntior,
                    Restaurants = await data.Restaurants.Where(r => r.RestaurateurId == GetUserId()).ToListAsync(),

                });
            }
            

            return View(employeeViewInfoModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(string username, string restaurantName)
        {
            var user = await userManager.FindByNameAsync(username);
            var restaurant =  await data.Restaurants.FirstAsync(r => r.Name == restaurantName);

            if (restaurant == null)
            {
                return BadRequest();
            }

            user.EMPRSID = restaurant.Id;
            await userManager.AddToRoleAsync(user, Employee);
            


            data.SaveChanges();
            return RedirectToAction(nameof(AllUsers));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromEmployee(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            await userManager.RemoveFromRoleAsync(user, Employee);

            user.EMPRSID = -1;

            data.SaveChanges();
            return RedirectToAction(nameof(AllUsers));
        }

        public async Task<IActionResult> Confirm(string id)
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

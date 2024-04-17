using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.Admin.Models;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models.ReservationView;
using System.Security.Claims;
using static Project.Constants.RoleConstants;
using static Project.Constants.MessageConstants;


namespace Project.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> users;

        public ReservationController(ApplicationDbContext _data, UserManager<ApplicationUser> _users)
        {
            data = _data;
            users = _users;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(string username, string restaurantName)
        {
            if (restaurantName == null)
            {
                return BadRequest();
            }
            if (username == null)
            {
                return BadRequest();
            }
            var user = await users.FindByNameAsync(username);
            var restaurant = await data.Restaurants.FirstAsync(r => r.Name == restaurantName);

            if (restaurant == null)
            {
                return BadRequest();
            }

            user.EMPRSID = restaurant.Id;
            await users.AddToRoleAsync(user, Employee);



            data.SaveChanges();
            return RedirectToAction("AllUsers", "Reservation");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromEmployee(string username)
        {
            var user = await users.FindByEmailAsync(username);
            var restaurant = await data.Restaurants.FindAsync(user.EMPRSID);

            if (restaurant.RestaurateurId != GetUserId())
            {
                TempData[UserMessageError] = "Не може да го премахнеш";
                return RedirectToAction("AllUsers", "Reservation");
            }
            await users.RemoveFromRoleAsync(user, Employee);

            user.EMPRSID = -1;

            data.SaveChanges();

            TempData[UserMessageSuccess] = "Премахна работника";

            return RedirectToAction("AllUsers", "Reservation");
        }

        public async Task<IActionResult> Confirm(string id)
        {

            var restaurateur = await users.FindByIdAsync(id);
            var restaurateurRequests = await data.RestaurateursRequests.FirstAsync(o => o.RestaurateurId == id);

            if (restaurateur != null)
            {
                await users.AddToRoleAsync(restaurateur, Constants.RoleConstants.Restaurateur);


                restaurateur.FirstName = restaurateurRequests.FirstName;
                restaurateur.LastName = restaurateurRequests.LastName;
                restaurateur.PhoneNumber = restaurateurRequests.PhoneNumber;

                data.RestaurateursRequests.Remove(restaurateurRequests);
                await data.SaveChangesAsync();
            }

            TempData[UserMessageSuccess] = "Confirm!";

            return RedirectToAction(nameof(AllReservation));
        }

        public async Task<IActionResult> AllUsers()
        {
            var Allusers = await users.Users.ToListAsync();

            var employeeViewInfoModels = new List<EmployeeInfoViewModel>();

            foreach (var user in Allusers)
            {
                var isEmployee = await users.IsInRoleAsync(user, Employee);
                var IsRestauntior = await users.IsInRoleAsync(user, Restaurateur);


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
        public async Task<IActionResult> Reservation(string name, string surname, string phone, string date, string start, string end, int restaurantId)
        {
            var reservation = new Reservation()
            {
                RestaurantId = restaurantId,
                UserId = GetUserId(),
                Date = date,
                Start = start,
                End = end,
                
            };

            var user = await users.FindByIdAsync(GetUserId());
            user.PhoneNumber = phone;
            user.FirstName = name;
            user.LastName = surname;


            await data.Reservations.AddAsync(reservation);
            await data.SaveChangesAsync();

            TempData[UserMessageSuccess] = "Направихте резервация успоешно";

            return RedirectToAction("Details", "Restaurant", new { id = restaurantId });
        }

        [HttpGet]
        public async Task<IActionResult> AllReservation()
        {

            var user = await users.FindByIdAsync(GetUserId());
            var reservations = await data.Reservations
                .Where(r => r.RestaurantId == user.EMPRSID)
                .Select(r => new ReservationInfoViewModel
                {
                    FirstName = r.User.FirstName,
                    LastName = r.User.LastName,
                    PhoneNumber = r.User.PhoneNumber,
                    Date = r.Date,
                    Start = r.Start,
                    End = r.End,
                })
                .ToListAsync();

            return View(reservations);

        }

        [HttpPost]
        public async Task<IActionResult> Yes(string phonenumber)
        {

            var reservation = await data.Reservations.FirstAsync(o => o.User.PhoneNumber == phonenumber);

            data.Reservations.Remove(reservation);

            data.SaveChanges();
            return RedirectToAction(nameof(AllReservation));
        }
        

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}

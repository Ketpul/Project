using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using Project.Models.ReservationView;
using System.Security.Claims;

namespace Project.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<ApplicationUser> users;
        public ReservationController(ApplicationDbContext _data, UserManager<ApplicationUser> _users)
        {
            data = _data;
            users = _users;
        }



        [HttpPost]
        public async Task<IActionResult> Reservation(string date, string start, string end, int restaurantId)
        {
            var reservation = new Reservation()
            {
                RestaurantId = restaurantId,
                UserId = GetUserId(),
                Date = date,
                Start = start,
                End = end,
            };

            await data.Reservations.AddAsync(reservation);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(AllReservation));
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

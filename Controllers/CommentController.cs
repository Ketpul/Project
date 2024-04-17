using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using System.Security.Claims;

namespace Project.Controllers
{
    public class CommentController : BaseController
    {
        private readonly ApplicationDbContext data;

        public CommentController(ApplicationDbContext _data)
        {
            data = _data;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentText, int restaurantId, double rating)
        {
            var restaurant = await data.Restaurants.FindAsync(restaurantId);
            var user = await data.Users.FirstAsync(u => u.Id == GetUserId());

            var comment = new Comment()
            {
                info = commentText,
                UserName = user.UserName,
                RestaurantId = restaurantId,
                Rating =  rating
            };
            await data.Comments.AddAsync(comment);

            double commentsAvg = 0;
            var comments = await data.Comments.Where(c => c.RestaurantId == restaurantId).ToListAsync();

            if (comments.Any())
            {
                commentsAvg = comments.Average(c => c.Rating);
            }

            restaurant.AvgRating = (int)commentsAvg;

            await data.SaveChangesAsync();


            return RedirectToAction("Details", "Restaurant", new { id = restaurantId });
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}

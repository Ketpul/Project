﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data.Models;
using Project.Data.SeedDb;
using System.Security.Claims;

namespace Project.Controllers
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext data;

        public CommentController(ApplicationDbContext _data)
        {
            data = _data;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentText, int restaurantId)
        {
            var user = await data.Users.FirstAsync(u => u.Id == GetUserId());

            var comment = new Comment()
            {
                info = commentText,
                UserName = user.UserName,
                RestaurantId = restaurantId
            };

            await data.Comments.AddAsync(comment);
            await data.SaveChangesAsync();

            return RedirectToAction();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
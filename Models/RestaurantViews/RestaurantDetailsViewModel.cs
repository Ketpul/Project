using Microsoft.AspNetCore.Mvc;
using Project.Data.Models;

namespace Project.Models.RestaurantViews
{
    public class RestaurantDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<string> ImageUrl { get; set; } = new List<string>();

        public string Address { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string RestaurateurId { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Commentar { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public string userCommentName { get; set; } = string.Empty;
        public string GoogleMaps { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

    }
}

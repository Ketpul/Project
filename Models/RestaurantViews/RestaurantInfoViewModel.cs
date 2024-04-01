using Project.Models.OtherViews;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.RestaurantViews
{
    public class RestaurantInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Owner { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

    }
}

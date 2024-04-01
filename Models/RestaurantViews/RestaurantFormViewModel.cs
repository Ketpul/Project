using Project.Data.Models;
using Project.Models.OtherViews;
using System.ComponentModel.DataAnnotations;

namespace Project.Models.RestaurantViews
{
    public class RestaurantFormViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 10)]

        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 10)]

        public string Description { get; set; } = string.Empty;

        public string Owner { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}

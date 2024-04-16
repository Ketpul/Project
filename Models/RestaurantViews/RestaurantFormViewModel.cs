using Project.Data.Models;
using Project.Enums;
using Project.Models.OtherViews;
using System.ComponentModel.DataAnnotations;
using static Project.Constants.DateConstants;

namespace Project.Models.RestaurantViews
{
    public class RestaurantFormViewModel
    {
        [Required]
        [StringLength(RestaurantNameMaxLength, MinimumLength = RestaurantNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl1 { get; set; } = string.Empty;
        [Required]
        public string ImageUrl2 { get; set; } = string.Empty;
        [Required]
        public string ImageUrl3 { get; set; } = string.Empty;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string GoogleMaps { get; set; } = string.Empty;

        public string Restaurateur { get; set; } = string.Empty;

        [Required]
        public RegionalCity RegionalCity { get; set; } 

        public string RegionalCityEnum { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}

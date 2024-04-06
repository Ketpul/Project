using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.OwnerViews
{
    public class RestaurateurRequestFromViewModel
    {
        public int Id { get; set; }
        public string RestaurateurId { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Information { get; set; } = string.Empty;

        [Required]
        public DateTime DateTime { get; set; }
    }
}

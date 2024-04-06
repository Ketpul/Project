using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Data.Models
{
    public class RestaurateurRequest
    {
        [Required]
        public string RestaurateurId { get; set; } = string.Empty;

        [ForeignKey(nameof(RestaurateurId))]
        public ApplicationUser Restaurateur { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Information { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }


    }
}

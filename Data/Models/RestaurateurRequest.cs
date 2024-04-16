using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Project.Constants.DateConstants;

namespace Project.Data.Models
{
    public class RestaurateurRequest
    {
        [Required]
        public string RestaurateurId { get; set; } = string.Empty;

        [ForeignKey(nameof(RestaurateurId))]
        public ApplicationUser Restaurateur { get; set; } = null!;

        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserInformationMaxLength)]
        public string Information { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }


    }
}

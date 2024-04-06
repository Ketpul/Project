using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Project.Constants.DateConstants;

namespace Project.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(UserNameMaxLength)]
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(UserNameMaxLength)]
        [PersonalData]
        public string LastName { get; set; } = string.Empty;


    }
}
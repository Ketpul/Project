using Project.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Areas.Admin.Models
{
    public class RestaurateurInfoViewModel
    {
        public string RestaurateurId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Information { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateTime { get; set; }
    }
}

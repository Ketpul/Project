using Project.Data.Models;

namespace Project.Models.ReservationView
{
    public class EmployeeInfoViewModel
    {
        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public bool IsEmployee { get; set; }
        public bool IsRestauntior { get; set; }
    }
}

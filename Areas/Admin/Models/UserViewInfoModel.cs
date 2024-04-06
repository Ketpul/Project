namespace Project.Areas.Admin.Models
{
    public class UserViewInfoModel
    {
        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName  { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        public bool IsRestaurateur { get; set; }

        public bool IsOwner { get; set; }
    }
}

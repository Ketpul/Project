using System.Security.Claims;
using static Project.Constants.RoleConstants;

namespace Project.Extension
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRole);
        }
        public static bool IsRestaurateur(this ClaimsPrincipal user)
        {
            return user.IsInRole(Restaurateur);
        }
        public static bool IsOwner(this ClaimsPrincipal user)
        {
            return user.IsInRole(OwnerRole);
        }
    }
}

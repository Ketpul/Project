using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Project.Constants.RoleConstants;

namespace Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = AdminRole)]
    public class AdminBaseController : Controller
    {
        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}

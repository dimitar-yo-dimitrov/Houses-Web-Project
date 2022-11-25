using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ValidationConstants.Role;

namespace Houses.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    [Area("Administration")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

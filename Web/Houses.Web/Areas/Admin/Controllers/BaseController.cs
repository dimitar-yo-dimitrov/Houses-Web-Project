using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ValidationConstants.Role;

namespace Houses.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    [Area("Admin")]
    [Route("Admin/[controller]/[Action]/{id?}")]
    public class BaseController : Controller
    {
    }
}

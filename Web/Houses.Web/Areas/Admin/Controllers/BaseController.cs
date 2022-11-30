using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Houses.Common.GlobalConstants.ValidationConstants.AdminConstants;

namespace Houses.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = AdministratorRoleName)]
    [Area(AreaName)]
    public class BaseController : Controller
    {
    }
}

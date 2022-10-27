using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Controllers
{
    public class AccessController : BaseController
    {
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
    }
}

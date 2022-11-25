using Microsoft.AspNetCore.Mvc;

namespace Houses.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

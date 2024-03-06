using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

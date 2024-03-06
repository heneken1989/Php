using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

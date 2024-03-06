using ASP.NETCoreIdentityCustom.Core;
using ASP.NETCoreIdentityCustom.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class RoleController : Controller
    {
        IMessageService service;
        public  RoleController(IMessageService service)
        {
            this.service = service; 
        }
        public async Task<IActionResult> Index()
        {

          var result=  await service.CheckSpelling("chao ban ");
            Console.WriteLine($"resultlllllllllllllllllll:{result}");
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public IActionResult Manager()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}

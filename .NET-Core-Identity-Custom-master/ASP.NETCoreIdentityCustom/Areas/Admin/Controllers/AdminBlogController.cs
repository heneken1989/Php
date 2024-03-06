using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.Service;
using ASP.NETCoreIdentityCustom.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP.NETCoreIdentityCustom.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminBlogController : Controller
	{
		IBlogService sv;
		ApplicationDbContext ctx;
	

		public AdminBlogController(IBlogService sv, ApplicationDbContext ctx)
		{
			this.sv = sv;
			this.ctx = ctx;	
		
		}

		public  async Task<IActionResult> Index()
		{
			var blogs =await sv.FindAll();
			return View(blogs);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(BlogViewModel model,IFormFile formFile)
		{
        
			
			var u = await ctx.Users.FirstOrDefaultAsync(a=>a.UserName== User.Identity!.Name);
            Console.WriteLine($"bbbbbbbbbbbb{u.UserName}");
            model.User = u;

            if (ModelState.IsValid) 
			{
               
                int result = await sv.Create(model, formFile);
                if (result != 0)
                {
                    return RedirectToAction("Index");
                }
                return View(model);

            }
            return View(model);
        }
	}
}

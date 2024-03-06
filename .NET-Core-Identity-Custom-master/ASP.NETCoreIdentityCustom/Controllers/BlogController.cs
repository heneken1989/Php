using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.Service;
using ASP.NETCoreIdentityCustom.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreIdentityCustom.Controllers
{
    public class BlogController : Controller
    {
       
        IBlogService service;

		public BlogController(IBlogService service)
		{
			
            this.service = service;
		}

		public async Task<IActionResult> Index()
        {

            var blogsViewModel = await service.FindAll();
            return View(blogsViewModel);
        }

        public async Task<IActionResult> SingleBlog(int id)
        {
            var blog = await service.FindById(id); 
            return View(blog);
        }

   

       
    }
}

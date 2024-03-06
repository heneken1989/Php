using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using ASP.NETCoreIdentityCustom.Controllers;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Http;
using static ASP.NETCoreIdentityCustom.Core.Constants;


namespace ASP.NETCoreIdentityCustom.Areas.Admin.Controllers
{
    /* [Authorize]*/
    [Area("Admin")]
    public class AdminChatController : Controller
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext ctx;
        string ApiPath = "https://localhost:7189/api/Rooms/list";
        public AdminChatController(HttpClient client, ApplicationDbContext ctx) 
        { 
           this.client = client;
            this.ctx = ctx;
        }


        
        public async Task<IActionResult> Index()
        {
            string url = ApiPath;
            string json = await client.GetStringAsync(url);
           var  r = JsonConvert.DeserializeObject<List<RoomViewModel>>(json);
             var mes = await ctx.Messages
                .Where(a=>a.Seen==false)
                .ToListAsync();

            var viewModel = new IndexViewModel
            { 
                Rooms = r,
                UnseenMessages = mes,
               
            };
            return View(viewModel);
        }

        public async Task<IActionResult> CheckUnseenMessages()
        {
            string url = ApiPath;
            string json = await client.GetStringAsync(url);
            var r = JsonConvert.DeserializeObject<List<RoomViewModel>>(json);
            var mes = await ctx.Messages
               .Where(a => a.Seen == false)
               .ToListAsync();

            var viewModel = new IndexViewModel
            {
                Rooms = r,
                UnseenMessages = mes,
            };
          

            // Return the JSON data along with the partial view HTML
            return Json(viewModel);
        }
    }
}

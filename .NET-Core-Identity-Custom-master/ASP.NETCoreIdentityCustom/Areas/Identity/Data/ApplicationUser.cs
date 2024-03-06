using ASP.NETCoreIdentityCustom.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreIdentityCustom.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Blog>? Blogs { get; set; }    

    }
}

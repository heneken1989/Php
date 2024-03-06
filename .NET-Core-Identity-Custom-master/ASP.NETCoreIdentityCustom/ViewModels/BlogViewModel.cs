using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NETCoreIdentityCustom.ViewModels
{
    public class BlogViewModel
    {
    
        public int Id { get; set; }

        [Required]  
        public string? Title { get; set; }
        [Required]
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IFormFile? Photo { get; set; }    
        public string? ImageUrl { get; set; }
        public ApplicationUser? User { get; set; }
    }
}

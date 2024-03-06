using ASP.NETCoreIdentityCustom.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string? Title { get; set; }

        [Column(TypeName ="text")]
        public string? Content { get; set; }    
        public DateTime? CreatedAt { get; set; }
        [StringLength(255)]
        public string? ImageUrl { get; set; } 
        public  ApplicationUser? User { get; set; }

    }
}

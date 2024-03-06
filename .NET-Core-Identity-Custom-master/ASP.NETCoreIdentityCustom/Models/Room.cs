using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ASP.NETCoreIdentityCustom.Areas.Identity.Data;

namespace ASP.NETCoreIdentityCustom.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public ApplicationUser? Admin { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}

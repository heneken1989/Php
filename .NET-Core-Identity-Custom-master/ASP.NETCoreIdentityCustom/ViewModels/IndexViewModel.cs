using ASP.NETCoreIdentityCustom.Models;

namespace ASP.NETCoreIdentityCustom.ViewModels
{
    public class IndexViewModel
    {
        public List<RoomViewModel>? Rooms { get; set; }
        public List<Message>? UnseenMessages { get; set; }
    }
}

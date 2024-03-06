using AutoMapper;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModels;

namespace ASP.NETCoreIdentityCustom.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomViewModel>()
                .ForMember(dst => dst.Admin, opt => opt.MapFrom(x => x.Admin.UserName));

            CreateMap<RoomViewModel, Room>();
        }
    }
}

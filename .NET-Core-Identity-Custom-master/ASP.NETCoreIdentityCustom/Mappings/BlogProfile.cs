using AutoMapper;
using ASP.NETCoreIdentityCustom.Helpers;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModels;

namespace ASP.NETCoreIdentityCustom.Mappings
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogViewModel>()
                .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(x => x.ImageUrl))
			    .ForMember(dst => dst.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt))
				.ForMember(dst => dst.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dst => dst.User, opt => opt.MapFrom(x => x.User))
				.ForMember(dst => dst.Title, opt => opt.MapFrom(x => x.Title))
				.ForMember(dst => dst.Content, opt => opt.MapFrom(x => x.Content));

            CreateMap<BlogViewModel, Blog>();
        }
    }
}

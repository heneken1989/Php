﻿using AutoMapper;
using ASP.NETCoreIdentityCustom.Helpers;
using ASP.NETCoreIdentityCustom.Models;
using ASP.NETCoreIdentityCustom.ViewModels;

namespace ASP.NETCoreIdentityCustom.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(dst => dst.FromUserName, opt => opt.MapFrom(x => x.FromUser.UserName))
                .ForMember(dst => dst.FromFullName, opt => opt.MapFrom(x => x.FromUser.FullName))
                .ForMember(dst => dst.Room, opt => opt.MapFrom(x => x.ToRoom.Name))
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => x.FromUser.Avatar))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(x => BasicEmojis.ParseEmojis(x.Content)));

            CreateMap<MessageViewModel, Message>();
        }
    }
}
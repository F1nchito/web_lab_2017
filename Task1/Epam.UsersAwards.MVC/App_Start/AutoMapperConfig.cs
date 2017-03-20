
using AutoMapper;
using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVC.ViewModels;
using Epam.UsersAwards.MVC.ViewModels.Awards;
using Epam.UsersAwards.MVC.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epam.UsersAwards.MVC.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserEditVM, User>().ForMember(dest => dest.Photo, opt => opt.Ignore());
                cfg.CreateMap<UserCreateVM, User>().ForMember(dest => dest.ID, opt => opt.Ignore()).ForMember(dest => dest.Photo, opt => opt.Ignore());
                cfg.CreateMap<User, UserCreateVM>().ForMember(dest => dest.Photo, opt => opt.Ignore());
                cfg.CreateMap<User, UserEditVM>().ForMember(dest => dest.Photo, opt => opt.Ignore());
                cfg.CreateMap<AwardEditVM, Award>().ForMember(dest => dest.Image, opt => opt.Ignore());
                cfg.CreateMap<AwardCreateVM, Award>().ForMember(dest => dest.ID, opt => opt.Ignore()).ForMember(dest => dest.Image, opt => opt.Ignore());
                cfg.CreateMap<Award, AwardCreateVM>().ForMember(dest => dest.Image, opt => opt.Ignore());
                cfg.CreateMap<Award, AwardEditVM>().ForMember(dest => dest.Image, opt => opt.Ignore());
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
using AutoMapper;
using Epam.UsersAwards.Entities;
using Epam.UsersAwards.MVC.ViewModels;
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
            Mapper.Initialize(cfg => cfg.CreateMap<UserCreateVM, User>().ForMember(dest => dest.ID, opt => opt.Ignore()).ForMember(dest => dest.Photo, opt => opt.Ignore()));

            Mapper.AssertConfigurationIsValid();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL;
using DAL;
using CBTour.Models;

namespace CBTour.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<UserDM, UserSM>().ReverseMap();
                config.CreateMap<UserSM, UserVM>().ReverseMap();
                //cfg.AddProfile(new UserProfile());
                //cfg.AddProfile(new PostProfile());
            });
        }
    }
}
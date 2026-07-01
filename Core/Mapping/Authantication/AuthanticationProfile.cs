using AutoMapper;
using Core.Feature.Authantication.Command.Models;
using Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Authantication
{
    public  class AuthanticationProfile:Profile
    {
        public AuthanticationProfile()
        {
            CreateMap<User, RegisterCommand>()
                .ForMember(d => d.First_Name, c => c.MapFrom(s => s.FirstName))
                .ForMember(d => d.Last_Name, c => c.MapFrom(s => s.LastName)).ReverseMap();

             
        }

    }
}

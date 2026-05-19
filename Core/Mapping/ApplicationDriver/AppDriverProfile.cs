using AutoMapper;
using Core.Feature.ApplicationDriver.Command.Models;
using Data.Entity.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.ApplicationDriver
{
    public  class AppDriverProfile:Profile
    {
        public AppDriverProfile()
        {
            CreateMap<DriverApplication, AddApplicationDriverCommand>()
                .ForMember(d => d.LicenceImageUrl, c => c.Ignore()).ReverseMap();
        }
    }
}

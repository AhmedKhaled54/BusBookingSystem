using AutoMapper;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Feature.Buses.Command.Models;
using Core.Resolver.Buses;
using Core.Resolver.Users;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Buses
{
    public  class BusProfile:Profile
    {
        public BusProfile()
        {
            CreateMap<Bus, AddBusCommand>()
                .ForMember(c => c.Image, c => c.Ignore()).ReverseMap();

            CreateMap<Bus, GetAllBusesQueryResult>()
                .ForMember(d => d.Driver, c => c.MapFrom(s => s.Driver.FullName))
                .ForMember(d => d.Owner, c => c.MapFrom(s => s.Owner.UserName))
                .ForMember(d => d.Image, c => c.MapFrom<BusImageResover>());

            CreateMap<Bus, GetDeletedBusesQueryResult>()
                .ForMember(d => d.Driver, c => c.MapFrom(s => s.Driver.FullName))
                .ForMember(d => d.Owner, c => c.MapFrom(s => s.Owner.UserName))
                .ForMember(d => d.DeletedName, c => c.MapFrom<DeletedUserNameResolver>())
                .ForMember(d => d.Image, c => c.MapFrom<DeletedBusImageResolver>());
       

        }
    }
}

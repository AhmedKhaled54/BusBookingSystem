using AutoMapper;
using Core.Feature.Stations.Command.Models;
using Core.Feature.Stations.Query.Result;
using Core.Resolver.Users;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Station
{
    public  class StationProfile: Profile
    {
        public StationProfile()
        {
            CreateMap<Stations, CreateStationCommand>().ReverseMap();

            CreateMap<Stations, GetAllStationQueryResult>();

            CreateMap<Stations, GetDeleteStationQueryResult>()
                .ForMember(d => d.DeletedAt, c => c.MapFrom(s => s.DeletedAt))
                .ForMember(d => d.DeletedName, c => c.MapFrom<DeletedUserNameStationResolver>());

            CreateMap<UpdateStationCommand, Stations>()
                .ForAllMembers(c
                => c.Condition((s, d, m) => m != null && (!(m is string x) || !string.IsNullOrWhiteSpace(x))));
        }
    }
}

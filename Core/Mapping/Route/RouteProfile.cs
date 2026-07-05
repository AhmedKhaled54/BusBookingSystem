using AutoMapper;
using Core.Feature.Route.Command.Models;
using Core.Feature.Route.Query.Results;
using Core.Resolver.Users;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Route
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<Routes, CreateRouteCommand>()
                .ForMember(d => d.StartStatoin, c => c.MapFrom(s => s.StartStationId))
                .ForMember(d => d.EndStatoin, c => c.MapFrom(s => s.EndStationId))
                .ReverseMap();

            CreateMap<Routes, GetRoutesQueryResult>()
                .ForMember(d => d.StartStation, c => c.MapFrom(s => s.StartStation.Name))
                .ForMember(d => d.EndStation, c => c.MapFrom(s => s.EndStation.Name))
                .ForMember(d => d.Distance, c => c.MapFrom(s => s.DisTance));
          
            CreateMap<Routes, GetDeletedRoutesQueryResult>()
               .ForMember(d => d.StartStation, c => c.MapFrom(s => s.StartStationId))
               .ForMember(d => d.EndStation, c => c.MapFrom(s => s.EndStationId))
               .ForMember(d => d.DeletedName, c => c.MapFrom<DeletedUserNameRouteRelover>())
               .ReverseMap();

        }
    }
}

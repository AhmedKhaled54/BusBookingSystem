using AutoMapper;
using Core.Feature.ApplicationDriver.Query.Result;
using Core.Feature.Trips.Command.Models;
using Core.Feature.Trips.Query.Results;
using Core.Resolver.Trips;
using Core.Resolver.Users;
using Data.Entity;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Trips
{
    public class TripProfile : Profile
    {
        public TripProfile()
        {
            CreateMap<Trip, CreateTripCommand>().ReverseMap();

            CreateMap<Trip, _GetTripQueryResult>()
                .ForMember(d => d.BusModel, o => o.MapFrom(s => s.Bus.Model))
                .ForMember(d => d.BusImage, o => o.MapFrom<TripsWithBusImageResolver>())
                .ForMember(d => d.FromStation, o => o.MapFrom(s => s.Routes.StartStation.Name))
                .ForMember(d => d.ToStation, o => o.MapFrom(s => s.Routes.EndStation.Name))
                .ForMember(d => d.Capacity, o => o.MapFrom(s => s.Bus.Capacity))
                .ForMember(d => d.RemainingSeats, o =>
                o.MapFrom(s => s.Bus.Capacity -
                s.Bookings.Count(b => b.Status == BookingStatus.Confirmed)));



            CreateMap<Trip,UpdateTripCommand>().ReverseMap();

            CreateMap<Trip, GetDeletedTripQueryResult>()
                .ForMember(d => d.BusModel, o => o.MapFrom(s => s.Bus.Model))
                .ForMember(d => d.BusImage, o => o.MapFrom<DeletedTripBusImagesResolver>())
                .ForMember(d => d.FromStation, o => o.MapFrom(s => s.Routes.StartStation.Name))
                .ForMember(d => d.ToStation, o => o.MapFrom(s => s.Routes.EndStation.Name))
                .ForMember(d => d.Capacity, o => o.MapFrom(s => s.Bus.Capacity))
                .ForMember(d => d.RemainingSeats, o =>
                o.MapFrom(s => s.Bus.Capacity -
                s.Bookings.Count(b => b.Status == BookingStatus.Confirmed)))
                .ForMember(d => d.DeletedName, o => o.MapFrom<DeletedUserNameTripResolver>());

        }
    }
}

using AutoMapper;
using Core.Feature.Booking.Query.Results;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Bookings
{
    public  class BookingProfile:Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetMyBookingQueryResult>()
               .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
               .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.Payment.Status.ToString()))
               .ForMember(d => d.ArrivalTime, o => o.MapFrom(s => s.Trip.ArrivalTime))
               .ForMember(d => d.DepartureTime, o => o.MapFrom(s => s.Trip.DepartualTime))
               .ForMember(d => d.StartStation, o => o.MapFrom(s => s.Trip.Routes.StartStation.Name))
               .ForMember(d => d.EndStation, o => o.MapFrom(s => s.Trip.Routes.EndStation.Name))
               .ForMember(d => d.SeatsCount, o => o.MapFrom(s => s.BookingSeats.Count()))
               .ForMember(d => d.BookingNumber, o => o.MapFrom(s => s.BookingNumber))
               .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice));

        }
    }
}

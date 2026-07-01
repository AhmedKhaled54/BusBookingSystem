using AutoMapper;
using Core.Feature.Tickets.Query.Results;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Tickets
{
    public  class TicketsProfile:Profile
    {
        public TicketsProfile()
        {
            CreateMap<Ticket, GetTicketByBookingIdQueryResult>()
               .ForMember(d => d.TicketNumber, c => c.MapFrom(s => s.Number))
               .ForMember(d => d.TicketStatus, c => c.MapFrom(s => s.Status))
               .ForMember(d => d.IssuedAt, c => c.MapFrom(s => s.IssuedAt))
               .ForMember(d => d.From, c => c.MapFrom(s => s.Booking.Trip.Routes.StartStation.Name))
               .ForMember(d => d.To, c => c.MapFrom(s => s.Booking.Trip.Routes.EndStation.Name))
               .ForMember(d => d.CustomerName, c => c.MapFrom(s => s.Booking.User.FullName))
               .ForMember(d => d.CustomerPhone, c => c.MapFrom(s => s.Booking.User.PhoneNumber))
               .ForMember(d => d.TripDate, c => c.MapFrom(s => s.Booking.Trip.DepartualTime))
               .ForMember(d => d.SeatNo, c => c.MapFrom(s => s.Booking.BookingSeats.Select(c => c.Seats.Number).ToList()))
               .ForMember(d => d.Price, c => c.MapFrom(s => s.Booking.TotalPrice));
        }
    }
}

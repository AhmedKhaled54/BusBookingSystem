using Data.Entity;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BookingService
{
    public  interface IBookingServices
    {
      //  Task<string> GenerateBookingNumber();
        Task<Booking> CreateBooking(int tripid, int userid,decimal tripprice ,List<Seats> seats); 
        IQueryable<Booking> GetMyBookings(int userid ,BookingStatus status);
         Task<Booking> GetBookingById(int id);
         Task<bool> CancelBooking(int bookingId, int userId);

    }
}

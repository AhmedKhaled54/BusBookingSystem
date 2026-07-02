using Data.Entity;
using Data.Enums;
using Data.Identity;
using Infrastracture.Abstract;
using Infrastracture.Specifications.BookingSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BookingService
{
    public class BookingServices : IBookingServices
    {
        private readonly IUnitOfWork _UOW;
        public BookingServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }

     

        public async Task<Booking> CreateBooking(int tripid, int userid, decimal tripprice, List<Seats> seats)
        {
            var booking = new Booking
            {
                TripId = tripid,
                UserId = userid,
                BookingNumber = await GenerateBookingNumber(),
                Status = BookingStatus.Pending,
                TotalPrice = seats.Count * tripprice,
                BookingSeats = seats.Select(s => new BookingSeats
                {
                    SeatsId = s.Id,
                    Price = tripprice       ,
                    Status = SeatsType.Reserved
                }).ToList()
            };

            await _UOW.Repository<Booking>().AddAsync(booking);
            return booking;
        }

       

        public IQueryable<Booking> GetMyBookings(int userid, BookingStatus status)
        {
            var specs=new MyBookingSpecification(userid, status);
            return _UOW.Repository<Booking>().GetEntityWithSpecification(specs);
              
        }
        public async Task<Booking> GetBookingById(int id)
        {
            var specs = new BookingWithBookingSeatsSpecification(id);

            var booking = await _UOW.Repository<Booking>().GetEntityByIdSepecification(specs);
            return booking;
        }
        public Task<bool> CancelBooking(int bookingId, int userId)
        {
            throw new NotImplementedException();
        }
        private  Task<int> GenerateBookingNumber()
        {
            return Task.FromResult(
                Random.Shared.Next(100000, 999999)
            );
        }
        //private Task<string> GenerateBookingNumber()
        //    => Task.FromResult($"BKG-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}");
    }
}

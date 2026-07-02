using Data.Entity;
using Data.Enums;
using Infrastracture.Abstract;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Services.CachingServices.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.SeatsService
{
    public class SeatServices : ISeatServices
    {
        private readonly IUnitOfWork _UOW;
        private ICachServices _cach;
        public SeatServices(IUnitOfWork uOW, ICachServices cach)
        {
            _UOW = uOW;
            _cach = cach;
        }

       

        public async Task<IEnumerable<Seats>> GetIvalaibleSeats(int busid, List<int> seatsid)
        {
           var seats =_UOW.Repository<Seats>().GetPridicated(s => s.BusId == busid && s.IsAvailable == true && seatsid.Contains(s.Id));
            return await seats.ToListAsync();
        }

      

        public async Task<bool> SeatsReversed(int tripid, List<int> seatsid)
            =>await _UOW.Repository<BookingSeats> ()
              .IsAny(c=>c.Booking.TripId == tripid && seatsid.Contains(c.SeatsId)&&c.Booking.Status==BookingStatus.Confirmed);

        public async Task<bool> TryLockSeatsAsync(int tripId, List<int> seatIds, int userId, int minutes = 5)
        {
            #region before 
            //var locked = new List<int>();

            ////chach first if any seat is already locked by another user
            //foreach (var seatId in seatIds)
            //{
            //    var lockKey = GenerateLockKey(tripId, seatId);
            //    if (await _cach.GetCach(lockKey) != null && lockKey != userId.ToString())
            //    {
            //        locked.Add(seatId);
            //    }
            //}

            //if (locked.Any())
            //{   
            //    return false; // Some seats are already locked by others
            //}

            //// Lock the seats for the current user

            //foreach (var seatId in seatIds)
            //{
            //    var lockKey = GenerateLockKey(tripId, seatId);
            //    await _cach.SetResponse(lockKey, userId.ToString(), TimeSpan.FromMinutes(minutes));
            //}

            //return true; // Seats successfully locked

            #endregion

            var lockedkey = new List<string>();
            foreach(var seatid in seatIds)
            {
                var locked = GenerateLockKey(tripId, seatid);
                var acquired =await _cach.SetIfNotExistsAsync(locked,userId.ToString(),TimeSpan.FromMinutes(minutes));

                if (!acquired)
                {
                    foreach(var key in lockedkey)
                        await _cach.DeleteCach(key);
                    return false;
                }
                lockedkey.Add(locked);
            }
            return true;

        }
        public Task<bool> AreSeatsLockedByOthers(int tripId, List<int> seatIds, int userId)
        {
            
            foreach (var seatId in seatIds)
            {
                var lockKey = GenerateLockKey(tripId, seatId);
                var lockedBy = _cach.GetCach(lockKey).Result;
                if (lockedBy != null && lockedBy != userId.ToString())
                {
                    return Task.FromResult(true); // At least one seat is locked by another user
                }
            }
            return Task.FromResult(false); // No seats are locked by others
        }
        public Task ReleaseSeatsAsync(int tripId, List<int> seatIds, int? userId=null)
        {
            
            foreach (var seatId in seatIds)
            {
                var lockKey = GenerateLockKey(tripId, seatId);
                _cach.DeleteCach(lockKey);
            }
            return Task.CompletedTask;
        }


        private string GenerateLockKey(int tripId, int seatId)
            => $"lock_trip_{tripId}_seat_{seatId}";
    }
}

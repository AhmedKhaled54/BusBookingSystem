using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.SeatsService
{
    public interface ISeatServices
    {
        Task<IEnumerable<Seats>> GetIvalaibleSeats(int busid, List<int> seatsid);
        Task<bool> SeatsReversed(int tripid, List<int> seatsid);

        Task<bool> TryLockSeatsAsync(int tripId, List<int> seatIds, int userId, int minutes = 5);
        Task ReleaseSeatsAsync(int tripId, List<int> seatIds, int? userId=null);
        Task<bool> AreSeatsLockedByOthers(int tripId, List<int> seatIds, int userId);
    }
}

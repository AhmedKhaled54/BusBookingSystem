using Data.Entity;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.TripsServices
{
    public interface ITripeServices
    {
        Task<Trip> CreateTrip(Trip trip);
        Task<bool> BusTripConflict(int BusId, DateTime departure, DateTime arrival, int? exludedtripid =null);
        Task<bool> DriverTripConflict(int DriverId, DateTime departure, DateTime arrival, int? exludedtripid =null);
        IQueryable<Trip> GetTrips(string? Search);
        Task<Trip> GetTripById(int Id);
        bool UpdateTrip (Trip trip);
        Task<bool>IsTripExsit(int Id);
        void ChangeTripStatus(Trip trip, TripeStatus status);
        bool SoftDeleteTrip(Trip trip,int userid );
        IQueryable<Trip> GetTripDeleted();
        Task<Trip> GetDeletedTripById (int Id);
    }
}

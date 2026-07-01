using Data.Entity;
using Data.Enums;
using Infrastracture.Abstract;
using Infrastracture.Specifications.TripsSepcification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.TripsServices
{
    public class TripeServices : ITripeServices
    {
        private readonly IUnitOfWork _UOW;
        public TripeServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }



        public async Task<Trip> CreateTrip(Trip trip)
        {
            await _UOW.Repository<Trip>().AddAsync(trip);
            return trip;

        }

        public async Task<bool> DriverTripConflict(int DriverId, DateTime departure, DateTime arrival, int? exludedtripid = null)
           => !await _UOW.Repository<Trip>()
            .IsAny
            (
               c =>
                (exludedtripid == null || c.Id != exludedtripid) &&
               c.DriverId == DriverId &&
               c.DepartualTime < arrival &&
               c.ArrivalTime > departure &&
               c.Status != TripeStatus.Cancelled
            );

        public async Task<bool> BusTripConflict(int BusId, DateTime departure, DateTime arrival, int? exludedtripid = null)
            => !await _UOW.Repository<Trip>()
            .IsAny
            (
                c =>
                (exludedtripid == null || c.Id != exludedtripid) &&
                c.BusId == BusId &&
                c.DepartualTime < arrival &&
                c.ArrivalTime > departure &&
                c.Status != TripeStatus.Cancelled
            );

        public IQueryable<Trip> GetTrips(string? Search)
        {
            var trips = _UOW.Repository<Trip>()
                .GetEntityWithSpecification(new TripWithBus_Driver_RoutesSpecification(Search));
            return trips;
        }

        public async Task<Trip> GetTripById(int Id)
        => await _UOW.Repository<Trip>()
            .GetEntityByIdSepecification(new TripWithBus_Driver_RoutesSpecification(Id));

        public bool UpdateTrip(Trip trip)
        {
            _UOW.Repository<Trip>().Update(trip);
            return true;
        }

        public async Task<bool> IsTripExsit(int Id)
            => await _UOW.Repository<Trip>().IsAny(c => c.Id == Id);

        public void ChangeTripStatus(Trip trip, TripeStatus status)
        {
            if (trip.Status == TripeStatus.Completed)
                throw new Exception("You can't change the status of a completed trip");
            if (trip.Status == TripeStatus.Cancelled)
                throw new Exception("You can't change the status of a cancelled trip");
            trip.Status = status;
        }

        public bool SoftDeleteTrip(Trip trip, int userid)
        {

            trip.IsDeleted = true;
            trip.DeletedBy = userid;
            trip.DeletedAt = DateTime.UtcNow;
            return true;
        }

        public IQueryable<Trip> GetTripDeleted()
        {
            var trip = _UOW.Repository<Trip>().GetEntityWithSpecification
                (new TripWithBus_Driver_RoutesSpecification())
                .IgnoreQueryFilters().Where(c=>c.IsDeleted);
            return trip;
        }

        public async Task<Trip> GetDeletedTripById(int Id)
            =>await _UOW.Repository<Trip>().FindIgnoreQueryFilter(c=>c.IsDeleted&&c.Id==Id);
    }
}

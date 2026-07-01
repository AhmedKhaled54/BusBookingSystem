using Data.Entity;
using Infrastracture.Abstract;
using Infrastracture.Specifications.BusSpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BusService
{
    public class BusServices : IBusServices
    {
        private readonly IUnitOfWork _UOW;
        public BusServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task<Bus> CreateBus(Bus Bus)
        {
            for (var i = 1; i <= Bus.Capacity; i++)
            {
                Bus.Seats.Add(new Seats
                {
                    Number = i,
                    IsAvailable = true
                });
            }
            await _UOW.Repository<Bus>().AddAsync(Bus);
            return Bus;
        }



        public IQueryable<Bus> GetAllBuses(BusSpecification specs)
        {
            var spec = new BusWithUserAndDriverSpecification(specs);
            var result = _UOW.Repository<Bus>().GetEntityWithSpecification(spec);
            return result;
        }

        public async Task<Bus> GetBusById(int id)
        {
            //var bus = await _UOW.Repository<Bus>().FindAsync(c => c.Id == id);
            var spec = new BusWithUserAndDriverSpecification(id);
            var bus = await _UOW.Repository<Bus>().GetEntityByIdSepecification(spec);
            return bus;
        }

        public async Task<bool> PlateNumberExsit(string number)
           => await _UOW.Repository<Bus>().IsAny(c => c.PlateNumber == number);


        public bool DeleteBus(Bus bus)
        {
            _UOW.Repository<Bus>().Delete(bus);
            return true;
        }

        public bool SoftDeleteBus(Bus bus,int userid)
        {
            bus.IsDeleted = true;
            bus.DeletedBy=userid;
            bus.DeletedAt=DateTime.Now;
            return true;
        }

        public IQueryable<Bus> GetBusesDeleted()
        {
            var spec = new BusSpecification();
            var buses = new BusWithUserAndDriverSpecification(spec);
            var result = _UOW.Repository<Bus>().GetEntityWithSpecification(buses)
                .IgnoreQueryFilters().Where(c => c.IsDeleted);
            return result;

        }

        public async Task<Bus> GetDeletedBusById(int id)
        =>await _UOW.Repository<Bus>().FindIgnoreQueryFilter(c=>c.Id == id&&c.IsDeleted);
            
        
    }
}

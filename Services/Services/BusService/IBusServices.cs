using Data.Entity;
using Infrastracture.Specifications.BusSpecification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BusService
{
    public  interface IBusServices
    {
        Task<Bus> CreateBus(Bus Bus);
        Task<Bus>GetBusById (int id );
        Task<bool> PlateNumberExsit(string number);
        IQueryable<Bus>GetAllBuses(BusSpecification specs);
        IQueryable<Bus> GetBusesDeleted();
        bool DeleteBus(Bus bus);
        bool SoftDeleteBus(Bus bus, int userid);
        Task<Bus>GetDeletedBusById (int id); 
    }
}

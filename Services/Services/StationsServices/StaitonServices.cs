using Data.Entity;
using Infrastracture.Abstract;
using Microsoft.AspNetCore.Components.RenderTree;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.StationsServices
{
    public class StaitonServices : IStaitonServices
    {
        private readonly IUnitOfWork _UOW;

        public StaitonServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }


        public async Task CreateStation(Stations entity)
            => await _UOW.Repository<Stations>().AddAsync(entity);


        public async Task<IEnumerable<Stations>> GetStations()
        {
            var stations = await _UOW.Repository<Stations>().GetAllAsync();
            return stations;
        }

        public async Task<Stations> GetStationsById(int id)
        {
            var station = await _UOW.Repository<Stations>().FindAsync(c => c.Id == id);
            return station;
        }

        public async Task<bool> IsExsit(string name, string city)
        => await _UOW.Repository<Stations>().IsAny(c => c.Name == name && c.City == city);

        public bool SoftDeleteStation(Stations stations, int userid)
        {
            stations.IsDeleted = true;
            stations.DeletedBy = userid;
            stations.DeletedAt = DateTime.Now;
            return true;
        }
        public async Task<IEnumerable<Stations>> GetDeletedStation()
        {
            var r = await _UOW.Repository<Stations>().GetAllAsyncIgnoreQueryFilter();
            return r.Where(c => c.IsDeleted);
        }

        public bool Update(Stations stations)
        {
            _UOW.Repository<Stations>().Update(stations);
            return true;
        }

        public async Task<bool> IsExsitValidation(string name, string city, int id)
            =>await _UOW.Repository<Stations>().IsAny(c=>c.Name==name&&c.City==city&&c.Id!=id);

        public async Task<bool> StationExsit(int id)
            =>await _UOW.Repository<Stations>().IsAny(c=>c.Id==id);
    }
}

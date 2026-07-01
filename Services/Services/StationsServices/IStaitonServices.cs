using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.StationsServices
{
    public  interface IStaitonServices
    {
        Task CreateStation(Stations entity);
        Task<IEnumerable<Stations>>GetStations();
        Task<Stations>GetStationsById (int id);
        Task<bool> IsExsit(string name, string city);
        Task<bool> IsExsitValidation(string name, string city,int id );


        bool SoftDeleteStation(Stations stations, int userid);
        Task<IEnumerable<Stations>> GetDeletedStation();
        bool Update(Stations stations);
        
    }
}

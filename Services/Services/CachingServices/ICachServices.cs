using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CachingServices
{
    public  interface ICachServices
    {
        Task<string> GetCach(string Key);
        Task SetResponse(string key, object response, TimeSpan timelive);
        Task DeleteCach(string Key);
    }
}

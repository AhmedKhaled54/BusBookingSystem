using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CachingServices.Redis
{
    public  interface ICachServices
    {
        Task<string> GetCach(string Key);
        Task SetResponse(string key, object response, TimeSpan timelive);
        Task<bool> SetIfNotExistsAsync(string key ,string value ,TimeSpan timelive );
        Task DeleteCach(string Key);
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Services.CachingServices
{
    public class CachServices : ICachServices
    {
        private readonly IDatabase database;

        public CachServices(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();

        }

        public async Task<string> GetCach(string Key)
        {

            var response = await database.StringGetAsync(Key);
            if (response.IsNullOrEmpty)
                return null;

            return response;



        }


        public async  Task SetResponse(string key, object response, TimeSpan timelive)
        {
            if (key == null)
                return;
            var option =new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var Serialize = JsonSerializer.Serialize(response,option);
            await database.StringSetAsync(key, Serialize, timelive);

        }
        public async Task DeleteCach(string Key)
        {
            if (!string.IsNullOrEmpty(Key))
                await database.KeyDeleteAsync(Key);

            return;
        }

    }
}

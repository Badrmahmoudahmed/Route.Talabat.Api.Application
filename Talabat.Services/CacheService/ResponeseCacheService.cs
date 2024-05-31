using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Services.Contract;

namespace Talabat.Services.CacheService
{
    public class ResponeseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponeseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task  CacheResponseAsync(string key, object Responese, TimeSpan TimetoLife)
        {
            if (Responese is null) return;
            var serlizeoption = new JsonSerializerOptions() {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

            var serlizedResponse = JsonSerializer.Serialize(Responese);

            await _database.StringSetAsync(key, serlizedResponse, TimetoLife);

        }

        public async Task<string?> GetCachedResponse(string Key)
        {
            var response =  await _database.StringGetAsync(Key);

            if(response.IsNullOrEmpty) return null;

            return response;
        }
    }
}

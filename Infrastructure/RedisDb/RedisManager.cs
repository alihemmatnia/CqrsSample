using StackExchange.Redis;
using System.Text.Json;

using Microsoft.Extensions.Caching.Distributed;
using EventData = Infrastructure.Entities.EventData;

namespace Infrastructure.RedisDb
{
    public class RedisManager:IRedisManager
    {
        private readonly IDistributedCache _cache;
        private readonly IConnectionMultiplexer _redis;

        public RedisManager(IDistributedCache cache, IConnectionMultiplexer redis)
        {
            _cache = cache;
            _redis = redis;
        }

        public async Task CreateEventSource(Entities.EventData data)
        {
            var key = "ev_"+data.Id;
            var dataSerialize = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, dataSerialize, new DistributedCacheEntryOptions(){AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20)});
        }

        public async Task<List<EventData>> GetAllEventSource()
        {
            var redisKeys = _redis.GetServer("localhost", 6379).Keys(pattern: "ev_*")
                .AsQueryable().Select(p => p.ToString()).ToList();
            var result = new List<EventData>();

            foreach (var i in redisKeys)
            {
                result.Add(JsonSerializer.Deserialize<EventData>(await _cache.GetStringAsync(i)));
            }
            return result;
        }
    }
}

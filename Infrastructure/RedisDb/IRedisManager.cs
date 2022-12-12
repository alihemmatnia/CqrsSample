using EventData = Infrastructure.Entities.EventData;

namespace Infrastructure.RedisDb
{
    public interface IRedisManager
    {
        Task CreateEventSource(EventData data);
        Task<List<EventData>> GetAllEventSource();
    }
}

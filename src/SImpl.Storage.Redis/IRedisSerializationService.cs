using StackExchange.Redis;

namespace SImpl.Storage.Redis
{
    public interface IRedisSerializationService
    {
        RedisValue SerializeObject<T>(T entry);
        
        T DeserializeObject<T>(RedisValue entry);
    }
}
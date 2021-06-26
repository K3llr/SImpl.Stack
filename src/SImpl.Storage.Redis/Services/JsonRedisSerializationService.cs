using System.Text.Json;
using StackExchange.Redis;

namespace SImpl.Storage.Redis.Services
{
    public class JsonRedisSerializationService : IRedisSerializationService
    {
        public RedisValue SerializeObject<T>(T entry)
        {
            return new(JsonSerializer.Serialize(entry));
        }

        public T DeserializeObject<T>(RedisValue entry)
        {
            return JsonSerializer.Deserialize<T>(entry);
        }
    }
}
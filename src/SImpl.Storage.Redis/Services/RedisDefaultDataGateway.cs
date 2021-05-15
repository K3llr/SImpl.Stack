using Newtonsoft.Json;
using StackExchange.Redis;

namespace SImpl.Storage.Redis.Services
{
    public class RedisDefaultDataGateway : RedisDataGateway
    {
        public RedisDefaultDataGateway(IRedisConnectionProvider redisConnectionProvider) : base(redisConnectionProvider)
        {
        }

        protected override RedisValue SerializeObject<T>(T entry)
        {
            return new(JsonConvert.SerializeObject(entry));
        }

        protected override T DeserializeObject<T>(RedisValue entry)
        {
            return JsonConvert.DeserializeObject<T>(entry);
        }
    }
}
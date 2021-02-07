using System;

namespace SImpl.Storage.Redis.Services
{
    public class RedisDataGatewaySettings<TModel>
    {
        public int Database { get; set; }

        public string KeyPrefix { get; set; }

        public TimeSpan? Expiry { get; set; }
    }
}
using System;

namespace SImpl.Storage.KeyValue.Redis.Services
{
    public interface IRedisModelStorageSettings { }

    public class RedisModelStorageStorageSettings<TModel> : IRedisModelStorageSettings
    {
        public string ModelName => typeof(TModel).Name;
        
        public int Database { get; set; }

        public string KeyPrefix { get; set; }

        public TimeSpan? Expiry { get; set; }
    }
}
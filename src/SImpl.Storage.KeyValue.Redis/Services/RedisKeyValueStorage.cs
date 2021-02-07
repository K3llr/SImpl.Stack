using System.Linq;
using SImpl.Storage.Redis;
using SImpl.Storage.Redis.Services;

namespace SImpl.Storage.KeyValue.Redis.Services
{
    public class RedisKeyValueStorage<TKey, TModel> : IRedisKeyValueStorage<TKey, TModel>
    {
        private readonly IRedisDataGateway _redisGateway;
        private readonly RedisDataGatewaySettings<TModel> _settings;

        public RedisKeyValueStorage(IRedisDataGateway redisGateway, RedisDataGatewaySettings<TModel> settings)
        {
            _redisGateway = redisGateway;
            _settings = settings;
        }
        
        public TModel Fetch(TKey key)
        {
            return _redisGateway.Get<TModel>(_settings.Database, GenerateRedisKey(key));
        }

        public TModel[] Fetch(TKey[] keys)
        {
            return _redisGateway.GetMultiple<TModel>(_settings.Database, keys.Select(GenerateRedisKey).ToArray())
                .Where(x => x != null)
                .ToArray();
        }

        public void Store(TKey key, TModel model)
        {
            _redisGateway.Set(_settings.Database, GenerateRedisKey(key), model, _settings.Expiry);
        }

        public void Remove(TKey key)
        {
            _redisGateway.Remove(_settings.Database, GenerateRedisKey(key));
        }

        private string GenerateRedisKey(TKey key)
        {
            return $"{_settings.KeyPrefix}:{key.ToString()}";
        }
    }
}
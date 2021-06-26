using System.Linq;
using SImpl.Storage.Redis;

namespace SImpl.Storage.KeyValue.Redis.Services
{
    public class RedisKeyValueStorage<TKey, TModel> : IRedisKeyValueStorage<TKey, TModel>
    {
        private readonly IRedisDataGateway _redisGateway;
        private readonly RedisModelStorageStorageSettings<TModel> _storageStorageSettings;

        public RedisKeyValueStorage(IRedisDataGateway redisGateway, RedisModelStorageStorageSettings<TModel> storageStorageSettings)
        {
            _redisGateway = redisGateway;
            _storageStorageSettings = storageStorageSettings;
        }
        
        public TModel Fetch(TKey key)
        {
            return _redisGateway.Get<TModel>(_storageStorageSettings.Database, GenerateRedisKey(key));
        }

        public TModel[] Fetch(TKey[] keys)
        {
            return _redisGateway.GetMultiple<TModel>(_storageStorageSettings.Database, keys.Select(GenerateRedisKey).ToArray())
                .Where(x => x != null)
                .ToArray();
        }

        public void Store(TKey key, TModel model)
        {
            _redisGateway.Set(_storageStorageSettings.Database, GenerateRedisKey(key), model, _storageStorageSettings.Expiry);
        }

        public void Remove(TKey key)
        {
            _redisGateway.Remove(_storageStorageSettings.Database, GenerateRedisKey(key));
        }

        private string GenerateRedisKey(TKey key)
        {
            return $"{_storageStorageSettings.KeyPrefix}:{key.ToString()}";
        }
    }
}
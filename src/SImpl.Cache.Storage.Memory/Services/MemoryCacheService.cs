using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SImpl.Cache.Models;

namespace SImpl.Cache.Storage.Memory.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null)
        {
            if (info.NoCache)
            {
                return default;
            }

            return await _cache.GetOrCreateAsync(info.Key, entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.Add(timeToLive ?? info.TimeToLive ?? TimeSpan.FromMinutes(0));
                return factory.Invoke();
            });
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory)
        {
            if (info.NoCache)
            {
                return default;
            }

            return _cache.GetOrCreate(info.Key, entry =>
            {
                entry.AbsoluteExpiration = DateTime.Now.Add(info.TimeToLive ?? TimeSpan.FromMinutes(0));
                return factory.Invoke();
            });
        }

        public T Get<T>(CacheEntryInfo info)
        {
            if (info.NoCache)
            {
                return default;
            }

            var cacheKey = info.Key;

            var value = _cache.TryGetValue<T>(cacheKey, out var result)
                ? result
                : default;
            
            return value;
        }

        public T Set<T>(CacheEntryInfo cacheInfo, T value, TimeSpan? timeToLive = null)
        {
            if (value == null || cacheInfo.NoCache)
            {
                return default;
            }

            var cacheKey = cacheInfo.Key;
            var absoluteExpiration = DateTime.Now.Add(timeToLive ?? cacheInfo.TimeToLive ?? TimeSpan.FromMinutes(0));

            _cache.Set(cacheKey, value, absoluteExpiration);

            return value;
        }

        public void Remove(CacheEntryInfo cacheInfo)
        {
            var cacheKey = cacheInfo.Key;
            _cache.Remove(cacheKey);
        }
    }
}
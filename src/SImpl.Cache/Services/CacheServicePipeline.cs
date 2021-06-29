using System;
using System.Threading.Tasks;
using SImpl.Cache.Models;

namespace SImpl.Cache.Services
{
    public class CacheServicePipeline : ICacheService
    {
        private readonly CacheLayer _current;
        private readonly ICacheService _next;

        public CacheServicePipeline(CacheLayer current, ICacheService next)
        {
            _current = current;
            _next = next;
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null)
        {
            return await _current.CacheService.GetOrCreateAsync(
                info,
                _next is null
                    ? factory
                    : async () => await _next.GetOrCreateAsync(info, factory, timeToLive),
                timeToLive ?? _current.Options.TimeToLive);
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            return _current.CacheService.GetOrCreate(
                info,
                _next is null
                    ? factory
                    : () => _next.GetOrCreate(info, factory, timeToLive),
                timeToLive ?? _current.Options.TimeToLive);
        }

        public void Remove(CacheEntryInfo info)
        {
            _current.CacheService.Remove(info);
            _next?.Remove(info);
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            _current.CacheService.Set(info, value, _current.Options.TimeToLive);
            _next?.Set(info, value, _current.Options.TimeToLive);

            return value;
        }
    }
}
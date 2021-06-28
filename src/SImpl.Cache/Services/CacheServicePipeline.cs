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
            if (info.NoCache)
            {
                return default;
            }

            return await _current.CacheService.GetOrCreateAsync(info, () =>
            {
                if (_next is null)
                {
                    return factory.Invoke();
                }
                else
                {
                    return _next.GetOrCreateAsync(info, factory, timeToLive);
                }
            }, timeToLive);
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory)
        {
            if (info.NoCache)
            {
                return default;
            }

            return _current.CacheService.GetOrCreate(info, () =>
            {
                if (_next is null)
                {
                    return factory.Invoke();
                }
                else
                {
                    return _next.GetOrCreate(info, factory);
                }
            });
        }

        public TItem Get<TItem>(CacheEntryInfo info)
        {
            if (info.NoCache)
            {
                return default;
            }

            return _current.CacheService.GetOrCreate(info, () =>
            {
                if (_next is null)
                {
                    return default;
                }
                else
                {
                    return _next.Get<TItem>(info);
                }
            });
        }

        public void Remove(CacheEntryInfo info)
        {
            _current.CacheService.Remove(info);
            _next?.Remove(info);
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            if (value == null || info.NoCache)
            {
                return value;
            }

            _current.CacheService.Set(info, value, _current.Options.TimeToLive);
            _next?.Set(info, value, _current.Options.TimeToLive);

            return value;
        }
    }
}
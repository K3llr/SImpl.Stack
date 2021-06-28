using System;
using System.Threading.Tasks;
using SImpl.Cache.Models;

namespace SImpl.Cache
{
    public interface ICacheService
    {
        Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null);

        TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory);
        
        TItem Get<TItem>(CacheEntryInfo info);

        TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null);
        
        void Remove(CacheEntryInfo info);
    }
}
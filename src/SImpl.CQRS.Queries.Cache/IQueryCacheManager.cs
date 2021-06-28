using SImpl.Cache;
using SImpl.Cache.Models;

namespace SImpl.CQRS.Queries.Cache
{
    public interface IQueryCacheManager
    {
        CacheEntryInfo GetInfo<TQuery, TResult>(TQuery query)
            where TQuery : class, IQuery<TResult>;
        
        ICacheService GetCacheService();
    }
}
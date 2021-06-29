using SImpl.Cache;
using SImpl.Cache.Models;
using SImpl.CQRS.Queries.Cache.Module;

namespace SImpl.CQRS.Queries.Cache.Services
{
    public class QueryCacheManager : IQueryCacheManager
    {
        private readonly QueryCacheModuleConfig _config;
        private readonly IQueryCacheKeyService _queryCacheKeyService;
        private readonly ICacheServiceProvider _cacheServiceProvider;

        public QueryCacheManager(
            QueryCacheModuleConfig config,
            IQueryCacheKeyService queryCacheKeyService,
            ICacheServiceProvider cacheServiceProvider)
        {
            _config = config;
            _queryCacheKeyService = queryCacheKeyService;
            _cacheServiceProvider = cacheServiceProvider;
        }
        
        public CacheEntryInfo GetInfo<TQuery, TResult>(TQuery query) 
            where TQuery : class, IQuery<TResult>
        {
            var definition = _config.GetQueryCacheDefinition(typeof(TQuery));
            if (definition == null)
            {
                return CacheEntryInfo.NoCacheInstance;
            }

            return new CacheEntryInfo
            {
                Key = _queryCacheKeyService.GenerateKey<TQuery, TResult>(query),
                SourceObject = query,
                TimeToLive = definition.TimeToLive,
            };
        }

        public ICacheService GetCacheService()
        {
            return _cacheServiceProvider.GetCacheService();
        }
    }
}
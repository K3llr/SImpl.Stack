using System.Threading.Tasks;

namespace SImpl.CQRS.Queries.Cache.Services
{
    public class QueryCacheInvalidator : IQueryCacheInvalidator
    {
        private readonly IQueryCacheManager _queryCacheManager;

        public QueryCacheInvalidator(IQueryCacheManager queryCacheManager)
        {
            _queryCacheManager = queryCacheManager;
        }
        
        public Task RemoveCachedQuery<TQuery, TResult>(TQuery query) 
            where TQuery : class, IQuery<TResult>
        {
            var info = _queryCacheManager.GetInfo<TQuery, TResult>(query);
            if (info.NoCache)
            {
                return Task.CompletedTask;
            }
            
            var cacheService = _queryCacheManager.GetCacheService();
            cacheService.Remove(info);
            
            return Task.CompletedTask;
        }
    }
}
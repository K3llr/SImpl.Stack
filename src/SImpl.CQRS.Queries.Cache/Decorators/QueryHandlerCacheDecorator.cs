using System.Threading.Tasks;

namespace SImpl.CQRS.Queries.Cache.Decorators
{
    public class QueryHandlerCacheDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : class, IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _innerQueryHandler;
        private readonly IQueryCacheManager _queryCacheManager;

        public QueryHandlerCacheDecorator(
            IQueryHandler<TQuery, TResult> innerQueryHandler, 
            IQueryCacheManager queryCacheManager)
        {
            _innerQueryHandler = innerQueryHandler;
            _queryCacheManager = queryCacheManager;
        }
        
        public async Task<TResult> HandleAsync(TQuery query)
        { 
            var info = _queryCacheManager.GetInfo<TQuery, TResult>(query);
            if (info.NoCache)
            {
                return await _innerQueryHandler.HandleAsync(query);
            }
            
            var cacheService = _queryCacheManager.GetCacheService();
            return await cacheService.GetOrCreateAsync(info, async () => await _innerQueryHandler.HandleAsync(query));
        }
    }
}
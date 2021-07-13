using System.Threading.Tasks;

namespace SImpl.CQRS.Queries.Cache
{
    public interface IQueryCacheInvalidator
    {
        Task RemoveCachedQuery<TQuery, TResult>(TQuery query) 
            where TQuery : class, IQuery<TResult>;
    }
}
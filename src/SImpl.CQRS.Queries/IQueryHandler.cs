using System.Threading.Tasks;

namespace SImpl.CQRS.Queries
{
    public interface IQueryHandler
    {
        
    }
    
    public interface IQueryHandler<in TQuery,TResult> : IQueryHandler
        where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
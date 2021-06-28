namespace SImpl.CQRS.Queries.Cache
{
    public interface IQueryCacheKeyService
    {
        string GenerateKey<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>;
    }
}
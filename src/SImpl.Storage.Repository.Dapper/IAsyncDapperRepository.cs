using SImpl.Common;

namespace SImpl.Storage.Repository.Dapper
{
    public interface IAsyncDapperRepository<TEntity, in TId> : IAsyncRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
    }
}
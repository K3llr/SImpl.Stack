using SImpl.Common;

namespace SImpl.Storage.Repository
{
    public interface IInMemoryAsyncRepository<TEntity, in TId> : IAsyncRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        
    }
}
using SImpl.Common;

namespace SImpl.Storage.Repository
{
    public interface IInMemoryRepository<TEntity, in TId> : IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        
    }
}
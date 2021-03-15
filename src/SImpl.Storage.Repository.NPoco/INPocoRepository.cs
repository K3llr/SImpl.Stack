using SImpl.Common;

namespace SImpl.Storage.Repository.NPoco
{
    public interface INPocoRepository<TEntity, in TId> : IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        
    }
}
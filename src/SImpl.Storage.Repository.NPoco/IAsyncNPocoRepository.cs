using SImpl.Common;

namespace SImpl.Storage.Repository.NPoco
{
    public interface IAsyncNPocoRepository<TEntity, in TId> : IAsyncRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
    }
}
using SImpl.Common;
using SImpl.Storage.Repository;
namespace SImpl.Storage.Dapper
{
    public interface IDapperyRepository<TEntity, in TId> : IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        
    }
}
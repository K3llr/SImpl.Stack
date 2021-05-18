using SImpl.Common;

namespace SImpl.Storage.Repository.Dapper
{
    public interface IDapperRepository<TEntity, in TId> : IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        
    }
}
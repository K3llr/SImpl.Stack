using System.Collections.Generic;
using SImpl.Common;

namespace SImpl.Storage.Repository
{
    public interface IRepository<TEntity, in TId>
        where TEntity : IEntity<TId>
    {
        IEnumerable<TEntity> GetAll();
        void Delete(TId id);
        TEntity Get(TId id);
        int SaveRange(IEnumerable<TEntity> list);
        void Update(TEntity entity);
        void Insert(TEntity entity);
    }
}
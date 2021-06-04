using System.Collections.Generic;
using SImpl.Common;

namespace SImpl.Storage.Repository
{
    public interface IRepository<TEntity, in TId>
        where TEntity : IEntity<TId>
    {
        IEnumerable<TEntity> GetAll();
        void Delete(params TId[] ids);
        TEntity Get(TId id);
        void SaveRange(IEnumerable<TEntity> list);
        void Update(TEntity entity);
        void Insert(TEntity entity);
    }
}
using System.Collections.Generic;
using SImpl.Common;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Dapper
{
    public class DapperyRepository<TEntity, TId> : IDapperyRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly DapperStorage _storage;

        public DapperyRepository(DapperStorage storage)
        {
            _storage = storage;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _storage.GetAll<TEntity>();
        }

        public void Delete<TEntity>(TId id)
        {
            _storage.Delete<TEntity>(id);
        }

        public TEntity Get(TId id)
        {
            return _storage.Get<TEntity>(id);
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
            _storage.SaveRange<TEntity>(list);
        }

        public void Update(TEntity entity)
        {
            _storage.Update<TEntity>(entity);
        }

        public void Insert(TEntity entity)
        {
            _storage.Insert<TEntity>(entity);
        }
    }
}
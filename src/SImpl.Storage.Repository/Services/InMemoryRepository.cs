using System.Collections.Generic;
using SImpl.Common;

namespace SImpl.Storage.Repository.Services
{
    public class InMemoryRepository<TEntity, TId> : IInMemoryRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        private readonly IAsyncRepository<TEntity, TId> _repository = new InMemoryAsyncRepository<TEntity, TId>();
        
        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAllAsync().Result;
        }

        public void Delete<TEntity>(TId id)
        {
            _repository.DeleteAsync<TEntity>(id);
        }

        public TEntity Get(TId id)
        {
            return _repository.GetAsync(id).Result;
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
             _repository.SaveRangeAsync(list);
        }

        public void Update(TEntity entity)
        {
            _repository.UpdateAsync(entity);
        }

        public void Insert(TEntity entity)
        {
            _repository.InsertAsync(entity);
        }
    }
}
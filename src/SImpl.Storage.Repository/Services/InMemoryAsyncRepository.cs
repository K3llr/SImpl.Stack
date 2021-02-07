using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SImpl.Common;

namespace SImpl.Storage.Repository.Services
{
    public class InMemoryAsyncRepository<TEntity, TId> : IInMemoryAsyncRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        private readonly IDictionary<TId, TEntity> _storage = new Dictionary<TId, TEntity>();
        
        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_storage.Values.AsEnumerable());
        }

        public Task DeleteAsync(TId id)
        {
            if (_storage.ContainsKey(id))
            {
                _storage.Remove(id);
            }
            
            return Task.CompletedTask;
        }

        public Task<TEntity> GetAsync(TId id)
        {
            return _storage.ContainsKey(id) 
                ? Task.FromResult(_storage[id]) 
                : null;
        }

        public Task<int> SaveRangeAsync(IEnumerable<TEntity> list)
        {
            var i = 0;
            foreach (var entity in list)
            {
                if (_storage.ContainsKey(entity.Id))
                {
                    UpdateAsync(entity);
                }
                else
                {
                    InsertAsync(entity);
                }

                i = i + 1;
            }

            return Task.FromResult(i);
        }

        public Task UpdateAsync(TEntity entity)
        {
            if (entity.Id is null || entity.Id.Equals(default(TId)))
            {
                throw new InvalidOperationException("Cannot update entity. Entity has no id.");
            }

            if (!_storage.ContainsKey(entity.Id))
            {
                throw new InvalidOperationException("Cannot update entity. Entity does not exist");
            }
            else
            {
                _storage[entity.Id] = entity;
            }

            return Task.CompletedTask;
        }

        public Task InsertAsync(TEntity entity)
        {
            if (entity.Id is null || entity.Id.Equals(default(TId)))
            {
                throw new InvalidOperationException("Cannot insert entity. Entity has no id.");
            }
            
            if (_storage.ContainsKey(entity.Id))
            {
                throw new InvalidOperationException("Cannot insert entity. Entity already exist.");
            }
            
            _storage[entity.Id] = entity;
            
            return Task.CompletedTask;
        }
    }
}
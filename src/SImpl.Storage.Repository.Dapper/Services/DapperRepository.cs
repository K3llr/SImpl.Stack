using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using SImpl.Common;

namespace SImpl.Storage.Repository.Dapper.Services
{
    public class DapperRepository<TEntity, TId> : IDapperRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>, new()
    {
        protected readonly IDapperUnitOfWork UnitOfWork;

        public DapperRepository(IDapperUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return UnitOfWork.GetConnection().GetAll<TEntity>();
        }

        public void Delete(params TId[] ids)
        {
            UnitOfWork.GetConnection().Delete(ids.Select(id => new TEntity() { Id = id}).ToArray());
        }

        public TEntity Get(TId id)
        {
            return UnitOfWork.GetConnection().Get<TEntity>(id);
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
            UnitOfWork.GetConnection().Insert(list.ToArray());
        }

        public void Update(TEntity entity)
        {
            UnitOfWork.GetConnection().Update(entity);
        }

        public void Insert(TEntity entity)
        {
            UnitOfWork.GetConnection().Insert(entity);
        }
    }
}
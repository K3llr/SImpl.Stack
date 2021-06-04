using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using SImpl.Common;
using SImpl.Storage.Repository.Dapper.Helpers;

namespace SImpl.Storage.Repository.Dapper.Services
{
    public class DapperRepository<TEntity, TId> : IDapperRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>, new()
    {
        private readonly IDapperUnitOfWork _unitOfWork;

        public DapperRepository(IDapperUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.GetConnection().GetAll<TEntity>();
        }

        public void Delete(params TId[] ids)
        {
            _unitOfWork.GetConnection().DeleteById<TEntity>(ids.Select(id => new TEntity() { Id = id}));
        }

        public TEntity Get(TId id)
        {
            return _unitOfWork.GetConnection().Get<TEntity>(id);
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
            _unitOfWork.GetConnection().Insert(list.ToArray());
        }

        public void Update(TEntity entity)
        {
            _unitOfWork.GetConnection().Update(entity);
        }

        public void Insert(TEntity entity)
        {
            _unitOfWork.GetConnection().Insert(entity);
        }
    }
}
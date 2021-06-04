using System.Collections.Generic;
using NPoco;
using SImpl.Common;

namespace SImpl.Storage.Repository.NPoco.Services
{
    public class NPocoRepository<TEntity, TId> : INPocoRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly INPocoUnitOfWork _unitOfWork;


        public NPocoRepository(INPocoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _unitOfWork.GetConnection().Fetch<TEntity>();
        }

        public void Delete(TId[] ids)
        {
            foreach (var id in ids)
            {
                _unitOfWork.GetConnection().Delete<TEntity>(id);    
            }
        }

        public TEntity Get(TId id)
        {
            return _unitOfWork.GetConnection().SingleById<TEntity>(id);
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
            _unitOfWork.GetConnection().Insert(list);
        }

        public void Update(TEntity entity)
        {
            _unitOfWork.GetConnection().Update(entity);
        }
        
        public void Insert(TEntity entity)
        {
            if (_unitOfWork.GetConnection().IsNew(entity))
            {
                _unitOfWork.GetConnection().Insert(entity);
            }
            else
            {
                _unitOfWork.GetConnection().Update(entity);
            }
        }
    }
}
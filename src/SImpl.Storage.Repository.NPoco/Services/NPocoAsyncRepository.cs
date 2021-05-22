using System.Collections.Generic;
using System.Threading.Tasks;
using NPoco;
using SImpl.Common;

namespace SImpl.Storage.Repository.NPoco.Services
{
    public class NPocoAsyncRepository<TEntity, TId> : IAsyncNPocoRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly INPocoUnitOfWork _unitOfWork;

        public NPocoAsyncRepository(INPocoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _unitOfWork.GetConnection().FetchAsync<TEntity>();
        }

        public async Task DeleteAsync(TId id)
        {
            await _unitOfWork.GetConnection().DeleteAsync(id);
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await _unitOfWork.GetConnection().SingleByIdAsync<TEntity>(id);
        }

        public async Task SaveRangeAsync(IEnumerable<TEntity> list)
        {
            await _unitOfWork.GetConnection().InsertAsync(list);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _unitOfWork.GetConnection().UpdateAsync(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            if (await _unitOfWork.GetConnection().IsNewAsync(entity))
            {
                await _unitOfWork.GetConnection().InsertAsync(entity);
            }
            else
            {
                await _unitOfWork.GetConnection().UpdateAsync(entity);
            }
        }
    }
}
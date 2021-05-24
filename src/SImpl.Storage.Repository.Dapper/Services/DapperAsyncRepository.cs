using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using SImpl.Common;
using SImpl.Storage.Repository.Dapper.Helpers;

namespace SImpl.Storage.Repository.Dapper.Services
{
    public class DapperAsyncRepository<TEntity, TId>: IAsyncDapperRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>, new()
    {
        private readonly IDapperUnitOfWork _unitOfWork;


        public DapperAsyncRepository(IDapperUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
     
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _unitOfWork.GetConnection().GetAllAsync<TEntity>();
        }

        public async Task DeleteAsync(TId[] ids) 
        {
            await _unitOfWork.GetConnection().DeleteByIdAsync<TEntity>(ids.Select(id => new TEntity() { Id = id}));
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await _unitOfWork.GetConnection().GetAsync<TEntity>(id);
        }

        public async Task SaveRangeAsync(IEnumerable<TEntity> list)
        {
            await _unitOfWork.GetConnection().InsertAsync(list.ToArray());
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _unitOfWork.GetConnection().UpdateAsync(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _unitOfWork.GetConnection().InsertAsync(entity);
        }
    }
}
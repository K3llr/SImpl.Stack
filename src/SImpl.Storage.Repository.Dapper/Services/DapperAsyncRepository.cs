using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using SImpl.Common;

namespace SImpl.Storage.Repository.Dapper.Services
{
    public class DapperAsyncRepository<TEntity, TId>: IAsyncDapperRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>, new()
    {
        protected readonly IDapperUnitOfWork UnitOfWork;

        public DapperAsyncRepository(IDapperUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
     
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await UnitOfWork.GetConnection().GetAllAsync<TEntity>();
        }

        public async Task DeleteAsync(TId[] ids) 
        {
            await UnitOfWork.GetConnection().DeleteAsync(ids.Select(id => new TEntity() { Id = id}).ToArray());
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await UnitOfWork.GetConnection().GetAsync<TEntity>(id);
        }

        public async Task SaveRangeAsync(IEnumerable<TEntity> list)
        {
            await UnitOfWork.GetConnection().InsertAsync(list.ToArray());
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await UnitOfWork.GetConnection().UpdateAsync(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await UnitOfWork.GetConnection().InsertAsync(entity);
        }
    }
}
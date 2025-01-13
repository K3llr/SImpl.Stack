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

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await UnitOfWork.GetConnection().GetAllAsync<TEntity>(UnitOfWork.GetTransaction());
        }

        public virtual async Task DeleteAsync(TId[] ids)
        {
            await UnitOfWork.GetConnection().DeleteAsync(
                    ids.Select(id => new TEntity { Id = id}).ToArray(),
                    UnitOfWork.GetTransaction());
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            return await UnitOfWork.GetConnection().GetAsync<TEntity>(id, UnitOfWork.GetTransaction());
        }

        public virtual async Task SaveRangeAsync(IEnumerable<TEntity> list)
        {
            await UnitOfWork.GetConnection().InsertAsync(list.ToArray(), UnitOfWork.GetTransaction());
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await UnitOfWork.GetConnection().UpdateAsync(entity, UnitOfWork.GetTransaction());
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await UnitOfWork.GetConnection().InsertAsync(entity, UnitOfWork.GetTransaction());
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SImpl.Storage.Repository
{
    public interface IAsyncRepository<TEntity, in TId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task DeleteAsync(TId id);
        Task<TEntity> GetAsync(TId id);
        Task<int> SaveRangeAsync(IEnumerable<TEntity> list);
        Task UpdateAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
    }
}
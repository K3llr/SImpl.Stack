using System.Collections.Generic;
using System.Threading.Tasks;

namespace SImpl.Storage.Repository
{
    public interface IAsyncRepository<TEntity, in TId>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task DeleteAsync(TId[] ids);
        Task<TEntity> GetAsync(TId id);
        Task SaveRangeAsync(IEnumerable<TEntity> list);
        Task UpdateAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
    }
}
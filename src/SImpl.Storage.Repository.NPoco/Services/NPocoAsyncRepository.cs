using System.Collections.Generic;
using System.Threading.Tasks;
using NPoco;
using SImpl.Common;

namespace SImpl.Storage.Repository.NPoco.Services
{
    public class NPocoAsyncRepository<TEntity, TId> : IAsyncNPocoRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly IDatabaseNpocoFactory _connection;


        public NPocoAsyncRepository(IDatabaseNpocoFactory connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                return await dbConnection.FetchAsync<TEntity>();
            }
        }

        public async Task DeleteAsync<TEntity>(TId id)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                await dbConnection.DeleteAsync(id);
            }
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                return await dbConnection.SingleByIdAsync<TEntity>(id);
            }
        }

        public async Task SaveRangeAsync(IEnumerable<TEntity> list)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                dbConnection.InsertAsync(list);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                await dbConnection.UpdateAsync(entity);
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                if (await dbConnection.IsNewAsync(entity))
                {
                    await    dbConnection.InsertAsync(entity);
                }
                else
                {
                    await    dbConnection.UpdateAsync(entity);
                }
            }
        }
    }
}
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using SImpl.Common;
using SImpl.Storage.Dapper.Helpers;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Repository.Dapper.Services
{
    public class DapperAsyncRepository<TEntity, TId>: IAsyncDapperRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly IDbConnectionFactory _connection;


        public DapperAsyncRepository(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

     
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                return await  dbConnection.GetAllAsync<TEntity>();

            }
        }

        public async Task DeleteAsync<TEntity>(TId id) 
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                await dbConnection.DeleteByIdAsync<TEntity>(id);
            }
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                return await dbConnection.GetAsync<TEntity>(id);
            }
        }

        public async Task SaveRangeAsync(IEnumerable<TEntity> list)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                await dbConnection.InsertAsync(list.ToArray());
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                await dbConnection.UpdateAsync<TEntity>(entity);
            }
        }

        public async Task InsertAsync(TEntity entity)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                await dbConnection.InsertAsync<TEntity>(entity);
            }
        }
    }
}
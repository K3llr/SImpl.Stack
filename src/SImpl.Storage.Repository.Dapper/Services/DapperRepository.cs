using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper.Contrib.Extensions;
using SImpl.Common;
using SImpl.Storage.Dapper.Helpers;
using SImpl.Storage.Repository.Dapper;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Dapper
{
    public class DapperRepository<TEntity, TId> : IDapperRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly IDbConnectionFactory _connection;


        public DapperRepository(IDbConnectionFactory connection)
        {
            _connection = connection;
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                return dbConnection.GetAll<TEntity>();
                
            }
        }

        public void Delete<TEntity>(TId id)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                dbConnection.DeleteById<TEntity>(id);
            }
        }

        public TEntity Get(TId id)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                return dbConnection.Get<TEntity>(id);
            }
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                dbConnection.Insert(list.ToArray());
            }
        }

        public void Update(TEntity entity)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                dbConnection.Update<TEntity>(entity);
            }
        }

        public void Insert(TEntity entity)
        {
            using (IDbConnection dbConnection = _connection.CreateConnection())
            {
                dbConnection.Insert<TEntity>(entity);
            }
        }
    }
}
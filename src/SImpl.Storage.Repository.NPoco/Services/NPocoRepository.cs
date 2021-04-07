using System.Collections.Generic;
using NPoco;
using SImpl.Common;

namespace SImpl.Storage.Repository.NPoco.Services
{
    public class NPocoRepository<TEntity, TId> : INPocoRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        private readonly IDatabaseNpocoFactory _connection;


        public NPocoRepository(IDatabaseNpocoFactory connection)
        {
            _connection = connection;
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                return dbConnection.Fetch<TEntity>();
            }
        }

        public void Delete<TEntity>(TId id)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                dbConnection.Delete<TEntity>(id);
            }
        }

        public TEntity Get(TId id)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                return dbConnection.SingleById<TEntity>(id);
            }
        }

        public void SaveRange(IEnumerable<TEntity> list)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                dbConnection.Insert(list);
            }
        }

        public void Update(TEntity entity)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                dbConnection.Update(entity);
            }
        }
        
        public void Insert(TEntity entity)
        {
            using (IDatabase dbConnection = _connection.CreateConnection())
            {
                if (dbConnection.IsNew(entity))
                {
                    dbConnection.Insert(entity);
                }
                else
                {
                    dbConnection.Update(entity);
                }
            }
        }
    }
}
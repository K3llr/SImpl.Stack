using System;
using System.Collections.Generic;
using System.Data;
using Dommel;
using SImpl.Common;
using SImpl.Storage.Dapper.Helper;

namespace SImpl.Storage.Repository.Module
{
    public class DapperStorage : IDisposable
    {
        public IDbConnection  Connection { get; private set; }
        public DapperStorage(IDbConnection connection)
        {
            
        }
        public void Dispose()
        {
            Connection.Dispose();
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
          return Connection.GetAll<TEntity>();
        }

        public TEntity Get<TEntity>(object id) where TEntity : class
        {
            return Connection.Get<TEntity>(id);
        }

        public void Delete<TEntity>(object id)  
        {
            //to need to make helper to remove by id
            Connection.Delete<TEntity>(id);
        }

        public void SaveRange<TEntity>(IEnumerable<TEntity> list) where TEntity : class
        {
            //why we need int there ?!
            Connection.InsertAll(list);
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            Connection.Insert(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            Connection.Update(entity);
        }
    }
}
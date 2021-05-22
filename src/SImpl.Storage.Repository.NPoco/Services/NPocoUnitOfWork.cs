using System;
using System.Data;
using NPoco;

namespace SImpl.Storage.Repository.NPoco.Services
{
    public class NPocoUnitOfWork : INPocoUnitOfWork, IDisposable
    {
        private readonly IDatabase _database;
        private IDbTransaction _transaction;

        public NPocoUnitOfWork(IDatabaseFactory connectionFactory)
        {
            _database = connectionFactory.CreateConnection();
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new ApplicationException("Transaction in progress");
           
            _database.BeginTransaction();
            _transaction = _database.Transaction;
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void AbortTransaction()
        {
            _transaction.Dispose();
        }

        public IDatabase GetConnection()
        {
            return _database;
        }

        public void Dispose()
        {
            _database?.Dispose();
            _transaction?.Dispose();
        }
    }
}
using System;
using System.Data;

namespace SImpl.Storage.Repository.Dapper.Services
{
    public class DapperUnitOfWork : IDapperUnitOfWork, IDisposable
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        public DapperUnitOfWork(IConnectionFactory connectionFactory)
        {
            _dbConnection = connectionFactory.CreateConnection();
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction != null)
                throw new ApplicationException("Transaction in progress");

            if (_dbConnection.State == ConnectionState.Closed) _dbConnection.Open();

            _transaction = _dbConnection.BeginTransaction(isolationLevel);
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void AbortTransaction()
        {
            _transaction.Rollback();
        }

        public IDbConnection GetConnection()
        {
            return _dbConnection;
        }

        public IDbTransaction GetTransaction()
        {
            return _transaction;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
            _transaction?.Dispose();
        }
    }
}
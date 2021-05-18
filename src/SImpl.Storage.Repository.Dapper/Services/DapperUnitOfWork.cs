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

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new ApplicationException("Transaction in progress");
            
            _transaction = _dbConnection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void AbortTransaction()
        {
            _transaction.Dispose();
        }

        public IDbConnection GetConnection()
        {
            return _dbConnection;
        }

        public void Dispose()
        {
            _dbConnection?.Dispose();
            _transaction?.Dispose();
        }
    }
}
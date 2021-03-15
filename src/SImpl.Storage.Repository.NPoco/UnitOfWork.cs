using System;
using System.Data;

namespace SImpl.Storage.Repository.NPoco
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _transaction;

        public UnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
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
    }
}
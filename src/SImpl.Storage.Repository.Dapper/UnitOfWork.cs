using System;
using System.Data;
using SImpl.Storage.Repository;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Dapper
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction _transaction;
        private DapperStorage _dapperStorage;

        public UnitOfWork(DapperStorage dapperStorage)
        {
            _dapperStorage = dapperStorage;
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
                throw new ApplicationException("Transaction in progress");
            _transaction = _dapperStorage.Connection.BeginTransaction();
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
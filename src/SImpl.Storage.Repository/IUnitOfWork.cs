using System.Data;

namespace SImpl.Storage.Repository
{
    public interface IUnitOfWork
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void AbortTransaction(); 
    }
}
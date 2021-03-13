namespace SImpl.Storage.Repository
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void AbortTransaction(); 
    }
}
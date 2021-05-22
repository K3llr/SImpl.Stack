using NPoco;

namespace SImpl.Storage.Repository.NPoco
{
    public interface INPocoUnitOfWork : IUnitOfWork
    {
        IDatabase GetConnection();
    }
}
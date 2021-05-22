using NPoco;

namespace SImpl.Storage.Repository.NPoco
{
    public interface IDatabaseFactory
    {
        IDatabase CreateConnection();
    }
}
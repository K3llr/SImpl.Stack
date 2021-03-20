using NPoco;

namespace SImpl.Storage.Repository.NPoco
{
    public interface IDatabaseNpocoFactory
    {
        IDatabase CreateConnection();
    }
}
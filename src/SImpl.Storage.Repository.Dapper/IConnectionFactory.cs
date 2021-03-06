using System.Data;

namespace SImpl.Storage.Repository.Dapper
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
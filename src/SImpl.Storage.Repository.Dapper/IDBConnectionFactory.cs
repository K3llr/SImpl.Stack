using System.Data;

namespace SImpl.Storage.Repository.Dapper
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
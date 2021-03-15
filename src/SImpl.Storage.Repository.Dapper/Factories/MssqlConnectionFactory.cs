using System.Data;
using System.Data.SqlClient;

namespace SImpl.Storage.Repository.Dapper.Factories
{
    public class MssqlConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            return new SqlConnection();
        }
    }
}
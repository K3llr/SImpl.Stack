using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPoco;
using SImpl.Storage.Repository.NPoco.Module;

namespace SImpl.Storage.Repository.NPoco.Factories
{
    public class MssqlDatabaseFactory(IConfiguration configuration, NPocoRepositoryConfig config) : IDatabaseFactory
    {
        public IDatabase CreateConnection()
        {
            var connectionString = configuration.GetValue<string>($"ConnectionStrings:{config.ConnectionStringName}");  
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return new Database(sqlConnection);
        }

      
    }
}
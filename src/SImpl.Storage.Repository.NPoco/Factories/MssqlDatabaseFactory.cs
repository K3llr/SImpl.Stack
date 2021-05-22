using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPoco;
using SImpl.Storage.Repository.NPoco.Module;

namespace SImpl.Storage.Repository.NPoco.Factories
{
    public class MssqlDatabaseFactory : IDatabaseFactory
    {
        private readonly IConfiguration _configuration;
        private readonly NPocoRepositoryConfig _config;

        public MssqlDatabaseFactory(IConfiguration configuration, NPocoRepositoryConfig config)
        {
            _configuration = configuration;
            _config = config;
        }

        public IDatabase CreateConnection()
        {
            var connectionString = _configuration.GetValue<string>($"ConnectionStrings:{_config.ConnectionStringName}");  
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return new Database(sqlConnection);
        }

      
    }
}
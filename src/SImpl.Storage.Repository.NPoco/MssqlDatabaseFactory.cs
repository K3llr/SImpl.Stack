using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NPoco;
using SImpl.Storage.Repository.NPoco.Module;

namespace SImpl.Storage.Repository.NPoco
{
    public class MssqlDatabaseFactory : IDatabaseNpocoFactory
    {
        private IConfiguration _configuration;
        private readonly NPocoRepositoryConfig _config;

        public MssqlDatabaseFactory(IConfiguration configuration, NPocoRepositoryConfig config)
        {
            _configuration = configuration;
            _config = config;
        }

        private string ConnectionStringName { get; set; }
        public IDatabase CreateConnection()
        {
            var connectionString = _configuration.GetValue<string>($"ConnectionStrings:{_config.ConnectionStringName}");  
            var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return new Database(sqlConnection);
        }

      
    }
}
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using SImpl.Storage.Repository.Module;

namespace SImpl.Storage.Repository.Dapper.Factories
{
    public class MssqlConnectionFactory : IDbConnectionFactory
    {
        private IConfiguration _configuration;
        private readonly DapperRepositoryConfig _config;

        public MssqlConnectionFactory(IConfiguration configuration, DapperRepositoryConfig config)
        {
            _configuration = configuration;
            _config = config;
        }

        private string ConnectionStringName { get; set; }
        public IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetValue<string>($"ConnectionStrings:{_config.ConnectionStringName}");  

            return new SqlConnection(connectionString);
        }

    }
}
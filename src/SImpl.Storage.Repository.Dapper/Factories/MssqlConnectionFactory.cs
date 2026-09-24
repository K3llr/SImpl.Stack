using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SImpl.Storage.Repository.Dapper.Module;

namespace SImpl.Storage.Repository.Dapper.Factories
{
    public class MssqlConnectionFactory(IConfiguration configuration, DapperRepositoryConfig config) : IConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            var connectionString = configuration.GetValue<string>($"ConnectionStrings:{config.ConnectionStringName}");  

            return new SqlConnection(connectionString);
        }

    }
}
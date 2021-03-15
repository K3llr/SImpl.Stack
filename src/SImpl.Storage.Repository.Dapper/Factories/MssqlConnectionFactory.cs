using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace SImpl.Storage.Repository.Dapper.Factories
{
    public class MssqlConnectionFactory : IDbConnectionFactory
    {
    

        private string ConnectionStringName { get; set; } = "Simpl.Repository.Db";
        public IDbConnection CreateConnection()
        {
            var connectionString = "server=localhost,1434;database=ietTaggingService;user id=db;password='db'";  

            return new SqlConnection(connectionString);
        }

        public void SetConnectionString(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }
    }
}
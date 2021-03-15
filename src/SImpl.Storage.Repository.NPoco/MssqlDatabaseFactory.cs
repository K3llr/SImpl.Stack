using System.Data.SqlClient;
using NPoco;

namespace SImpl.Storage.Repository.NPoco
{
    public class MssqlDatabaseFactory : IDatabaseNpocoFactory
    {
        private string ConnectionStringName { get; set; } = "Simpl.Repository.Db";
        public IDatabase CreateConnection()
        {
            var connectionString = "server=localhost,1434;database=db;user id=sa;password='db'";
            var sqlConnection = new SqlConnection(connectionString);
            return new Database(sqlConnection);
        }

        public void SetConnectionString(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
        }
    }
}
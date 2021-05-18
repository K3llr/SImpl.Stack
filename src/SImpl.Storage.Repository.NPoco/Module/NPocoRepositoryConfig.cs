using SImpl.Common;
using SImpl.Storage.Repository.NPoco.Factories;

namespace SImpl.Storage.Repository.NPoco.Module
{
    public class NPocoRepositoryConfig
    {
        public TypeOf<IDatabaseFactory> DatabaseFactory { get; private set; } = TypeOf.New<IDatabaseFactory, MssqlDatabaseFactory>();
        public string ConnectionStringName { get; private set; } = "Simpl.Repository.Db";

        public NPocoRepositoryConfig SetConnectionFactory(TypeOf<IDatabaseFactory> databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            return this;
        }
        
        public NPocoRepositoryConfig SetConnectionStringName(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
            return this;
        }

    }
}
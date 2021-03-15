namespace SImpl.Storage.Repository.NPoco.Module
{
    public class NPocoRepositoryConfig
    {
        public IDatabaseNpocoFactory DbConnectionFactory
        {
            get;
            set;
        } = new MssqlDatabaseFactory();

        public string ConnectionStringName;



    }
    
}
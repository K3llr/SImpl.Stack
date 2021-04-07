using System;

namespace SImpl.Storage.Repository.NPoco.Module
{
    public class NPocoRepositoryConfig
    {
        public Type DbConnectionFactory
        {
            get;
            set;
        } = typeof(MssqlDatabaseFactory);

        public string ConnectionStringName= "Simpl.Repository.Db";



    }
    
}
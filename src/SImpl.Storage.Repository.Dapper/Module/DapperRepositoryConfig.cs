using System;
using SImpl.Storage.Repository.Dapper;
using SImpl.Storage.Repository.Dapper.Factories;

namespace SImpl.Storage.Repository.Module
{
    public class DapperRepositoryConfig
    {
        public Type DbConnectionFactory
        {
            get;
            set;
        } = typeof(MssqlConnectionFactory);
        public string ConnectionStringName;
    }
    
}
using System;
using SImpl.Storage.Repository.Dapper;
using SImpl.Storage.Repository.Dapper.Factories;

namespace SImpl.Storage.Repository.Module
{
    public class DapperRepositoryConfig
    {
        public IDbConnectionFactory DbConnectionFactory
        {
            get;
            set;
        } = new MssqlConnectionFactory();

        public string ConnectionStringName;



    }
    
}
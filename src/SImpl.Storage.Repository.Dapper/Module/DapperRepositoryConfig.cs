using System;
using SImpl.Common;
using SImpl.Storage.Repository.Dapper.Factories;

namespace SImpl.Storage.Repository.Dapper.Module
{
    public class DapperRepositoryConfig
    {
        public TypeOf<IConnectionFactory> ConnectionFactory { get; private set; } = TypeOf.New<IConnectionFactory, MssqlConnectionFactory>();
        public string ConnectionStringName { get; private set; }  = "Simpl.Repository.Db";

        public DapperRepositoryConfig SetConnectionFactory(TypeOf<IConnectionFactory> connectionFactory)
        {
            ConnectionFactory = connectionFactory;
            return this;
        }
        
        public DapperRepositoryConfig SetConnectionStringName(string connectionStringName)
        {
            ConnectionStringName = connectionStringName;
            return this;
        }

        public TypeMapper FluentMapping()
        {
            return TypeMapper.Mappings;
        }
    }
}
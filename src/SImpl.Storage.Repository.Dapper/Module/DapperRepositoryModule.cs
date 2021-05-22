using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Dapper;
using SImpl.Storage.Repository.Dapper;
using SImpl.Storage.Repository.Dapper.Factories;
using SImpl.Storage.Repository.Dapper.Services;
using SImpl.Storage.Repository.Services;

namespace SImpl.Storage.Repository.Module
{
    public class DapperRepositoryModule : IServicesCollectionConfigureModule
    {
        private readonly DapperRepositoryConfig _dapperRepoConfig;

        public DapperRepositoryModule(DapperRepositoryConfig dapperRepoConfig)
        {
            _dapperRepoConfig = dapperRepoConfig;
        }

        public string Name { get; } = nameof(DapperRepositoryModule);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<,>), typeof(DapperRepository<,>));
            services.AddSingleton(typeof(IDapperRepository<,>), typeof(DapperRepository<,>));
            services.AddSingleton(typeof(IAsyncRepository<,>), typeof(DapperAsyncRepository<,>));
            services.AddSingleton(typeof(IAsyncDapperRepository<,>), typeof(DapperAsyncRepository<,>));
            services.AddSingleton(typeof(DapperRepositoryConfig));
            services.AddSingleton(typeof(IDbConnectionFactory), _dapperRepoConfig.DbConnectionFactory);
            services.AddSingleton(typeof(IUnitOfWork),
                (services) => new UnitOfWork(services.GetService<IDbConnectionFactory>().CreateConnection()));
        }
    }
}
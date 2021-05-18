using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Repository.Dapper.Services;

namespace SImpl.Storage.Repository.Dapper.Module
{
    public class DapperRepositoryModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(DapperRepositoryModule);
        
        private readonly DapperRepositoryConfig _repositoryConfig;

        public DapperRepositoryModule(DapperRepositoryConfig repositoryConfig)
        {
            _repositoryConfig = repositoryConfig;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(DapperRepositoryConfig), _repositoryConfig);
            services.AddSingleton(typeof(IConnectionFactory), _repositoryConfig.ConnectionFactory.ImplType);
            
            services.AddScoped(typeof(IDapperRepository<,>), typeof(DapperRepository<,>));
            services.AddScoped(typeof(IRepository<,>), typeof(DapperRepository<,>));
            
            services.AddScoped(typeof(IAsyncDapperRepository<,>), typeof(DapperAsyncRepository<,>));
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(DapperAsyncRepository<,>));
            
            services.AddScoped<IDapperUnitOfWork, DapperUnitOfWork>();
            services.AddScoped(typeof(IUnitOfWork), s => s.GetRequiredService<IDapperUnitOfWork>());
        }
    }
}
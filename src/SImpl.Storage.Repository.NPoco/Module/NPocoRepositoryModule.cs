using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Repository.NPoco.Services;

namespace SImpl.Storage.Repository.NPoco.Module
{
    public class NPocoRepositoryModule : IServicesCollectionConfigureModule
    {
        private readonly NPocoRepositoryConfig _nPocoRepoConfig;

        public NPocoRepositoryModule(NPocoRepositoryConfig nPocoRepoConfig)
        {
            _nPocoRepoConfig = nPocoRepoConfig;
        }

        public string Name { get; } = nameof(NPocoRepositoryModule);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<,>), typeof(NPocoRepository<,>));
            services.AddSingleton(typeof(INPocoRepository<,>), typeof(NPocoRepository<,>));
            services.AddSingleton(typeof(IAsyncRepository<,>), typeof(NPocoAsyncRepository<,>));
            services.AddSingleton(typeof(IAsyncNPocoRepository<,>), typeof(NPocoAsyncRepository<,>));
            var factory = _nPocoRepoConfig.DbConnectionFactory;
            if (!string.IsNullOrWhiteSpace(_nPocoRepoConfig.ConnectionStringName))
            {
                factory.SetConnectionString(_nPocoRepoConfig.ConnectionStringName);
            }
            
            services.AddSingleton(typeof(IDatabaseNpocoFactory), factory);
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Repository.NPoco.Services;

namespace SImpl.Storage.Repository.NPoco.Module
{
    public class NPocoRepositoryModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(NPocoRepositoryModule);
        
        private readonly NPocoRepositoryConfig _repositoryConfig;

        public NPocoRepositoryModule(NPocoRepositoryConfig repositoryConfig)
        {
            _repositoryConfig = repositoryConfig;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(NPocoRepositoryConfig), _repositoryConfig);
            services.AddSingleton(typeof(IDatabaseFactory), _repositoryConfig.DatabaseFactory.ImplType);
            
            services.AddSingleton(typeof(INPocoRepository<,>), typeof(NPocoRepository<,>));
            services.AddSingleton(typeof(IRepository<,>), typeof(NPocoRepository<,>));

            services.AddSingleton(typeof(IAsyncNPocoRepository<,>), typeof(NPocoAsyncRepository<,>));
            services.AddSingleton(typeof(IAsyncRepository<,>), typeof(NPocoAsyncRepository<,>));
         
            services.AddScoped<INPocoUnitOfWork, NPocoUnitOfWork>();
            services.AddScoped(typeof(IUnitOfWork), s => s.GetRequiredService<INPocoUnitOfWork>());
        }
    }
}
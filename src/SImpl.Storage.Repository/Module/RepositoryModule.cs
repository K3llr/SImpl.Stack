using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.Repository.Services;

namespace SImpl.Storage.Repository.Module
{
    public class RepositoryModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(RepositoryModule);

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IRepository<,>), typeof(InMemoryRepository<,>));
            services.AddSingleton(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));
            services.AddSingleton(typeof(IAsyncRepository<,>), typeof(InMemoryAsyncRepository<,>));
            services.AddSingleton(typeof(IInMemoryAsyncRepository<,>), typeof(InMemoryAsyncRepository<,>));
        }
    }
}
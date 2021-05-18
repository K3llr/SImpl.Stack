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
            services.AddSingleton(typeof(IInMemoryRepository<,>), typeof(InMemoryRepository<,>));
            services.AddSingleton(typeof(IRepository<,>), s => s.GetRequiredService(typeof(IInMemoryRepository<,>)));
            
            services.AddSingleton(typeof(IInMemoryAsyncRepository<,>), typeof(InMemoryAsyncRepository<,>));
            services.AddSingleton(typeof(IAsyncRepository<,>), s => s.GetRequiredService(typeof(IInMemoryAsyncRepository<,>)));
        }
    }
}
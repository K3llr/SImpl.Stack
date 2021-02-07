using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Storage.KeyValue.Services;

namespace SImpl.Storage.KeyValue.Module
{
    public class KeyValueModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(KeyValueModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IKeyValueStorage<,>), typeof(InMemoryKeyValueStorage<,>));
            services.AddSingleton(typeof(IInMemoryKeyValueStorage<,>), typeof(InMemoryKeyValueStorage<,>));

        }
    }
}
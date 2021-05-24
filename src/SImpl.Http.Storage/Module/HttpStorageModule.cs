using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace SImpl.Http.Storage.Module
{
    public class HttpStorageModule : IServicesCollectionConfigureModule
    {
        public HttpStorageModuleConfig Config { get; }

        public HttpStorageModule(HttpStorageModuleConfig config)
        {
            Config = config;
        }
        
        public string Name => nameof(HttpStorageModule);
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Transactional>();

            if (Config.TransactionPerRequestsEnabled)
            {
                services
                    .AddMvc(setup =>
                    {
                        setup.Filters.AddService<Transactional>(1);
                    });
            }
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using SImpl.Cache.Services;
using SImpl.Modules;

namespace SImpl.Cache.Module
{
    public class CacheModule : IServicesCollectionConfigureModule
    {
        public CacheModuleConfig Config { get; }

        public CacheModule(CacheModuleConfig config)
        {
            Config = config;
        }

        public string Name => nameof(CacheModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config);
            services.AddSingleton<ICacheServiceProvider, CacheServiceProvider>();
            services.AddSingleton<ICacheLayerCacheServiceFactory, CacheLayerCacheServiceFactory>(provider => new CacheLayerCacheServiceFactory(provider));
        }
    }
}
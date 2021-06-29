using Microsoft.Extensions.DependencyInjection;
using SImpl.Cache.Logging.Decorators;
using SImpl.Cache.Logging.Services;
using SImpl.Cache.Models;
using SImpl.Cache.Module;
using SImpl.Host.Builders;
using SImpl.Modules;

namespace SImpl.Cache.Logging.Module
{
    [DependsOn(typeof(CacheModule))]
    public class PreLoggingCacheModule : IHostBuilderConfigureModule, IServicesCollectionConfigureModule, ICacheLayerModule
    {
        public PreLoggingCacheModule(CacheLayerDefinition layerDefinition)
        {
            LayerDefinition = layerDefinition;
        }
        
        public string Name => nameof(PreLoggingCacheModule);
        
        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseCache();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Decorate<ICacheLayerCacheServiceFactory, CacheLayerCacheServiceFactoryDecorator>();

            services.AddSingleton<PreLoggingCacheService>();
        }

        public CacheLayerDefinition LayerDefinition { get; }
    }
}
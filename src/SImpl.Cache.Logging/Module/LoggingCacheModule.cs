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
    public class LoggingCacheModule : IHostBuilderConfigureModule, IServicesCollectionConfigureModule, ICacheLayerModule
    {
        public LoggingCacheModule(CacheLayerDefinition layerDefinition)
        {
            LayerDefinition = layerDefinition;
        }
        
        public string Name => nameof(LoggingCacheModule);
        
        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseCache();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PreLoggingCacheService>();
            services.AddSingleton<PostLoggingCacheService>();

            services.Decorate<ICacheLayerCacheServiceFactory, CacheLayerCacheServiceFactoryDecorator>();
        }

        public CacheLayerDefinition LayerDefinition { get; }
    }
}
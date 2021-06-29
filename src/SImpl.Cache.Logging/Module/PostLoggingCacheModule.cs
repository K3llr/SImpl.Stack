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
    public class PostLoggingCacheModule : IHostBuilderConfigureModule, IServicesCollectionConfigureModule, ICacheLayerModule
    {
        public PostLoggingCacheModule(CacheLayerDefinition layerDefinition)
        {
            LayerDefinition = layerDefinition;
        }
        
        public string Name => nameof(PostLoggingCacheModule);
        
        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseCache();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PostLoggingCacheService>();
        }

        public CacheLayerDefinition LayerDefinition { get; }
    }
}
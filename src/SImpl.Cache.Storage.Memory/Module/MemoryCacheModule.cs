using Microsoft.Extensions.DependencyInjection;
using SImpl.Cache.Models;
using SImpl.Cache.Module;
using SImpl.Cache.Storage.Memory.Services;
using SImpl.Host.Builders;
using SImpl.Modules;

namespace SImpl.Cache.Storage.Memory.Module
{
    [DependsOn(typeof(CacheModule))]
    public class MemoryCacheModule : IHostBuilderConfigureModule, IServicesCollectionConfigureModule, ICacheLayerModule
    {
        public MemoryCacheModule(CacheLayerDefinition layerDefinition)
        {
            LayerDefinition = layerDefinition;
        }
        
        public string Name => nameof(MemoryCacheModule);
        
        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.UseCache();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
        }

        public CacheLayerDefinition LayerDefinition { get; }
    }
}
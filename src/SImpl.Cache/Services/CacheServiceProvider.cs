using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Cache.Models;
using SImpl.Cache.Module;

namespace SImpl.Cache.Services
{
    public class CacheServiceProvider : ICacheServiceProvider
    {
        private readonly CacheModuleConfig _config;
        private readonly ICacheLayerCacheServiceFactory _cacheLayerCacheServiceFactory;

        private ICacheService _firstCacheService;

        public CacheServiceProvider(CacheModuleConfig config, ICacheLayerCacheServiceFactory cacheLayerCacheServiceFactory)
        {
            _config = config;
            _cacheLayerCacheServiceFactory = cacheLayerCacheServiceFactory;
        }
        
        public ICacheService GetCacheService()
        {
            return _firstCacheService ??= CreateCacheServiceChain();
        }

        private ICacheService CreateCacheServiceChain()
        {
            // Init current to null
            var current = default(ICacheService);

            // Reverse order of caching layers in order to initialize from the bottom up 
            var layersDefinitions = _config.CacheLayerDefinitions.Reverse();
            foreach (var layerDefinition in layersDefinitions)
            {
                var cacheService = _cacheLayerCacheServiceFactory.Create(layerDefinition);
                var cacheLayer = new CacheLayer
                {
                    CacheService = cacheService,
                    Options = layerDefinition.Options
                };
                
                current = new CacheServicePipeline(cacheLayer, current);
            }

            return current;
        }
    }
}
using System;
using System.Collections.Generic;
using SImpl.Cache.Models;
using SImpl.Host.Builders;

namespace SImpl.Cache.Module
{
    public class CacheModuleConfig
    {
        private readonly ISImplHostBuilder _hostBuilder;
        private readonly List<CacheLayerDefinition> _cacheLayerDefinitions = new();

        public IReadOnlyList<CacheLayerDefinition> CacheLayerDefinitions => _cacheLayerDefinitions.AsReadOnly();
        
        public CacheModuleConfig(ISImplHostBuilder hostBuilder)
        {
            _hostBuilder = hostBuilder;
        }
        
        public TCacheExtension AttachCacheExtension<TCacheExtension>(Func<TCacheExtension> extensionFactory)
            where TCacheExtension : ICacheExtensionModule
        {
            return _hostBuilder.AttachNewOrGetConfiguredModule(extensionFactory);
        }

        public CacheModuleConfig AddCacheLayer<TCacheStorage>(Func<TCacheStorage> layerFactory, Action<CacheLayerDefinition> configure)
            where TCacheStorage : ICacheLayerModule
        {
            var layer = _hostBuilder.AttachNewOrGetConfiguredModule(layerFactory);
            configure?.Invoke(layer.LayerDefinition);
            
            _cacheLayerDefinitions.Add(layer.LayerDefinition);
            
            return this;
        }
    }

}
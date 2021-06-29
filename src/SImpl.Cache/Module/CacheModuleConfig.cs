using System;
using System.Collections.Generic;
using SImpl.Cache.Models;
using SImpl.Host.Builders;

namespace SImpl.Cache.Module
{
    public class CacheModuleConfig
    {
        private readonly ISImplHostBuilder _hostBuilder;
        private CacheLayerDefinition _firstCacheLayerDefinition;
        private CacheLayerDefinition _lastCacheLayerDefinition;
        private readonly List<CacheLayerDefinition> _cacheLayerDefinitions = new();

        public IReadOnlyList<CacheLayerDefinition> CacheLayerDefinitions
        {
            get
            {
                var cacheLayerDefinitions = new List<CacheLayerDefinition>();
                
                if (_firstCacheLayerDefinition != null)
                {
                    cacheLayerDefinitions.Add(_firstCacheLayerDefinition);
                }
                
                cacheLayerDefinitions.AddRange(_cacheLayerDefinitions);

                if (_lastCacheLayerDefinition != null)
                {
                    cacheLayerDefinitions.Add(_lastCacheLayerDefinition);
                }
                
                return cacheLayerDefinitions.AsReadOnly();
            }
        }

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
        
        public CacheModuleConfig SetFirstCacheLayer<TCacheStorage>(Func<TCacheStorage> layerFactory, Action<CacheLayerDefinition> configure)
            where TCacheStorage : ICacheLayerModule
        {
            var layer = _hostBuilder.AttachNewOrGetConfiguredModule(layerFactory);
            configure?.Invoke(layer.LayerDefinition);
            
            _firstCacheLayerDefinition = layer.LayerDefinition;
            
            return this;
        }
        
        public CacheModuleConfig SetLastCacheLayer<TCacheStorage>(Func<TCacheStorage> layerFactory, Action<CacheLayerDefinition> configure)
            where TCacheStorage : ICacheLayerModule
        {
            var layer = _hostBuilder.AttachNewOrGetConfiguredModule(layerFactory);
            configure?.Invoke(layer.LayerDefinition);
            
            _lastCacheLayerDefinition = layer.LayerDefinition;
            
            return this;
        }
    }

}
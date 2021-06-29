using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Cache.Models;

namespace SImpl.Cache.Services
{
    public class CacheLayerCacheServiceFactory : ICacheLayerCacheServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CacheLayerCacheServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public ICacheService Create(CacheLayerDefinition definition)
        {
            return (ICacheService) _serviceProvider.GetRequiredService(definition.CacheServiceType.ImplType);
        }
    }
}
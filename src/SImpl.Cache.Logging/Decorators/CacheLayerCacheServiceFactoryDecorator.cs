using System;
using Microsoft.Extensions.Logging;
using SImpl.Cache.Logging.Attributes;
using SImpl.Cache.Models;

namespace SImpl.Cache.Logging.Decorators
{
    public class CacheLayerCacheServiceFactoryDecorator : ICacheLayerCacheServiceFactory
    {
        private readonly ICacheLayerCacheServiceFactory _innerFactory;
        private readonly ILogger<CacheServiceDecorator> _logger;

        public CacheLayerCacheServiceFactoryDecorator(
            ICacheLayerCacheServiceFactory innerFactory,
            ILogger<CacheServiceDecorator> logger)
        {
            _innerFactory = innerFactory;
            _logger = logger;
        }
        
        public ICacheService Create(CacheLayerDefinition definition)
        {
            var cacheService = _innerFactory.Create(definition);

            return Attribute.GetCustomAttribute(cacheService.GetType(), typeof(DisableLoggingAttribute)) != null
                ? cacheService
                : new CacheServiceDecorator(cacheService, _logger);
        }
    }
}
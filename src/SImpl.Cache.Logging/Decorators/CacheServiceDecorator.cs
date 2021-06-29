using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.Cache.Logging.Attributes;
using SImpl.Cache.Models;

namespace SImpl.Cache.Logging.Decorators
{
    public class CacheServiceDecorator : ICacheService
    {
        private readonly ICacheService _innerCacheService;
        private readonly ILogger<CacheServiceDecorator> _logger;
        
        private readonly string _cacheServiceName;
        private readonly bool _disable;

        public CacheServiceDecorator(ICacheService innerCacheService, ILogger<CacheServiceDecorator> logger)
        {
            _innerCacheService = innerCacheService;
            _logger = logger;
            
            var cacheServiceType = _innerCacheService.GetType();
            _cacheServiceName = cacheServiceType.Name;

            if (Attribute.GetCustomAttribute(cacheServiceType, typeof(DisableLoggingAttribute)) != null)
            {
                _disable = true;
            }
        }
        
        public async Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null)
        {
            Log($"GetOrCreateAsync:Begin:{info.Key}");
            var result = await _innerCacheService.GetOrCreateAsync(info, factory, timeToLive);
            Log($"GetOrCreateAsync:End:{info.Key}");

            return result;
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            Log($"GetOrCreate:Begin:{info.Key}");
            var result = _innerCacheService.GetOrCreate(info, factory, timeToLive);
            Log($"GetOrCreate:End:{info.Key}");

            return result;
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            Log($"Set:Begin:{info.Key}");
            var result = _innerCacheService.Set(info, value, timeToLive);
            Log($"Set:End:{info.Key}");

            return result;
        }

        public void Remove(CacheEntryInfo info)
        {
            Log($"Remove:Begin:{info.Key}");
            _innerCacheService.Remove(info);
            Log($"Remove:End:{info.Key}");
        }

        private void Log(string logMessage)
        {
            if (_disable)
            {
                _logger.LogDebug($"{_cacheServiceName}:{logMessage}");
            }
        }
    }
}
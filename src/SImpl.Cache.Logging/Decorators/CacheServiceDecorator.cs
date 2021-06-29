using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.Cache.Models;

namespace SImpl.Cache.Logging.Decorators
{
    public class CacheServiceDecorator : ICacheService
    {
        private readonly ICacheService _innerCacheService;
        private readonly ILogger<CacheServiceDecorator> _logger;
        
        private readonly string _cacheServiceName;

        public CacheServiceDecorator(ICacheService innerCacheService, ILogger<CacheServiceDecorator> logger)
        {
            _innerCacheService = innerCacheService;
            _logger = logger;
            
            var cacheServiceType = _innerCacheService.GetType();
            _cacheServiceName = cacheServiceType.Name;
        }
        
        public async Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null)
        {
            var cacheHit = true;
            
            Log(info, $"GetOrCreateAsync:Begin");
            var result = await _innerCacheService.GetOrCreateAsync(info, () =>
            {
                cacheHit = false;
                Log(info, $"GetOrCreateAsync:FactoryInvoke");
                return factory.Invoke();
            }, timeToLive);

            if (cacheHit)
            {
                Log(info, $"GetOrCreateAsync:>>HIT<<");    
            }
            
            Log(info, $"GetOrCreateAsync:End");

            return result;
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            var cacheHit = true;
            
            Log(info, $"GetOrCreate:Begin");
            var result = _innerCacheService.GetOrCreate(info, () =>
            {
                cacheHit = false;
                Log(info, $"GetOrCreate:FactoryInvoke");
                return factory.Invoke();
            }, timeToLive);
            
            if (cacheHit)
            {
                Log(info, $"GetOrCreate:>>HIT<<");    
            }
            
            Log(info, $"GetOrCreate:End");

            return result;
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            Log(info, $"Set:Begin");
            var result = _innerCacheService.Set(info, value, timeToLive);
            Log(info, $"Set:End");

            return result;
        }

        public void Remove(CacheEntryInfo info)
        {
            Log(info, $"Remove:Begin");
            _innerCacheService.Remove(info);
            Log(info, $"Remove:End");
        }

        private void Log(CacheEntryInfo info, string logMessage)
        {
            _logger.LogDebug($"{info.Key}:{_cacheServiceName}:{logMessage}");
        }
    }
}
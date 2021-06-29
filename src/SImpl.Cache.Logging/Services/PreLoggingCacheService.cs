using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.Cache.Logging.Attributes;
using SImpl.Cache.Models;

namespace SImpl.Cache.Logging.Services
{
    [DisableLogging]
    public class PreLoggingCacheService : ICacheService
    {
        private readonly ILogger<PreLoggingCacheService> _logger;
        private readonly string _cacheServiceName;

        public PreLoggingCacheService(ILogger<PreLoggingCacheService> logger)
        {
            _logger = logger;
            _cacheServiceName = nameof(PreLoggingCacheService);
        }
        
        public async Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null)
        {
            if (info.NoCache)
            {
                Log($"GetOrCreateAsync:NoCache:{info.Key}");
                return default;
            }
            
            return await factory.Invoke();
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            if (info.NoCache)
            {
                Log($"GetOrCreate:NoCache:{info.Key}");
                return default;
            }
            
            return factory.Invoke();
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            if (info.NoCache)
            {
                Log($"Set:NoCache:{info.Key}");
                return value;
            }
            
            return value;
        }

        public void Remove(CacheEntryInfo info)
        {
            if (info.NoCache)
            {
                Log($"Remove:NoCache:{info.Key}");
            }
        }
        
        private void Log(string logMessage)
        {
            _logger.LogDebug($"{_cacheServiceName}:{logMessage}");
        }
    }
}
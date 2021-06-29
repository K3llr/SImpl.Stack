using System;
using System.Text.Json;
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
            Log($"GetOrCreateAsync:Key:{info.Key}:{info.SourceObject.GetType().Name}{JsonSerializer.Serialize(info)}");
            
            if (info.NoCache)
            {
                Log($"GetOrCreateAsync:NoCache:{info.Key}");
            }

            var item = await factory.Invoke();
            
            Log($"GetOrCreateAsync:End:{info.Key}");
            return item;
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            Log($"GetOrCreate:Key:{info.Key}:{info.SourceObject.GetType().Name}{JsonSerializer.Serialize(info)}");

            if (info.NoCache)
            {
                Log($"GetOrCreate:NoCache:{info.Key}");
            }

            var item = factory.Invoke();
            
            Log($"GetOrCreate:End:{info.Key}");
            return item;
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            Log($"Set:Key:{info.Key}:{info.SourceObject.GetType().Name}{JsonSerializer.Serialize(info)}");

            if (info.NoCache)
            {
                Log($"Set:NoCache:{info.Key}");
            }
            
            Log($"Set:End:{info.Key}");
            return value;
        }

        public void Remove(CacheEntryInfo info)
        {
            Log($"Remove:Key:{info.Key}:{info.SourceObject.GetType().Name}{JsonSerializer.Serialize(info)}");

            if (info.NoCache)
            {
                Log($"Remove:NoCache:{info.Key}");
            }
            
            Log($"Remove:End:{info.Key}");
        }
        
        private void Log(string logMessage)
        {
            _logger.LogDebug($"{_cacheServiceName}:{logMessage}");
        }
    }
}
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
            Log(info, $"GetOrCreateAsync:Source:{info.SourceObject.GetType().Name}");
            Log(info, $"GetOrCreateAsync:Info:{JsonSerializer.Serialize(info)}");
            
            if (info.NoCache)
            {
                Log(info, $"GetOrCreateAsync:NoCache");
            }

            var item = await factory.Invoke();
            
            Log(info, $"GetOrCreateAsync:End");
            return item;
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            Log(info, $"GetOrCreate:Source:{info.SourceObject.GetType().Name}");
            Log(info, $"GetOrCreate:Info:{JsonSerializer.Serialize(info)}");

            if (info.NoCache)
            {
                Log(info, $"GetOrCreateAsync:NoCache");
            }

            var item = factory.Invoke();
            
            Log(info, $"GetOrCreate:End");
            return item;
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            Log(info, $"Set:Source:{info.SourceObject.GetType().Name}");
            Log(info, $"Set:Info:{JsonSerializer.Serialize(info)}");

            if (info.NoCache)
            {
                Log(info, $"GetOrCreateAsync:NoCache");
            }
            
            Log(info, $"GetOrCreate:Set");
            return value;
        }

        public void Remove(CacheEntryInfo info)
        {
            Log(info, $"Remove:Source:{info.SourceObject.GetType().Name}");
            Log(info, $"Remove:Info:{JsonSerializer.Serialize(info)}");

            if (info.NoCache)
            {
                Log(info, $"GetOrCreateAsync:NoCache");
            }
            
            Log(info, $"GetOrCreate:Remove");
        }
        
        private void Log(CacheEntryInfo info, string logMessage)
        {
            _logger.LogDebug($"{info.Key}:{_cacheServiceName}:{logMessage}");
        }
    }
}
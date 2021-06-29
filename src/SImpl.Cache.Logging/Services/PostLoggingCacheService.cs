using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SImpl.Cache.Logging.Attributes;
using SImpl.Cache.Models;

namespace SImpl.Cache.Logging.Services
{
    [DisableLogging]
    public class PostLoggingCacheService : ICacheService
    {
        private readonly ILogger<PostLoggingCacheService> _logger;
        private readonly string _cacheServiceName;

        public PostLoggingCacheService(ILogger<PostLoggingCacheService> logger)
        {
            _logger = logger;
            _cacheServiceName = nameof(PostLoggingCacheService);
        }
        
        public async Task<TItem> GetOrCreateAsync<TItem>(CacheEntryInfo info, Func<Task<TItem>> factory, TimeSpan? timeToLive = null)
        {
            Log($"GetOrCreateAsync:Miss:{info.Key}");
            Log($"EndOfPipeline:{info.Key}");
            return await factory.Invoke();
        }

        public TItem GetOrCreate<TItem>(CacheEntryInfo info, Func<TItem> factory, TimeSpan? timeToLive = null)
        {
            Log($"GetOrCreate:Miss:{info.Key}");
            Log($"EndOfPipeline:{info.Key}");
            return factory.Invoke();
        }

        public TItem Set<TItem>(CacheEntryInfo info, TItem value, TimeSpan? timeToLive = null)
        {
            Log($"EndOfPipeline:{info.Key}");
            return value;
        }

        public void Remove(CacheEntryInfo info)
        {
            Log($"EndOfPipeline:{info.Key}");
        }
        
        private void Log(string logMessage)
        {
            _logger.LogDebug($"{_cacheServiceName}:{logMessage}");
        }
    }
}
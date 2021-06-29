using SImpl.Cache.Logging.Module;
using SImpl.Cache.Logging.Services;
using SImpl.Cache.Models;
using SImpl.Cache.Module;
using SImpl.Common;

namespace SImpl.Cache.Logging
{
    public static class BuilderExtensions
    {
        public static CacheModuleConfig AddCacheLayerLogging(this CacheModuleConfig cacheModuleConfig)
        {
            cacheModuleConfig.SetFirstCacheLayer(() => new LoggingCacheModule(new CacheLayerDefinition()), definition =>
            {
                definition.CacheServiceType = TypeOf.New<ICacheService, PreLoggingCacheService>();
            });
            
            cacheModuleConfig.SetLastCacheLayer(() => new LoggingCacheModule(new CacheLayerDefinition()), definition =>
            {
                definition.CacheServiceType = TypeOf.New<ICacheService, PostLoggingCacheService>();
            });
            
            return cacheModuleConfig;
        }
    }
}
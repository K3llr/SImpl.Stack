using System;
using SImpl.Cache.Models;
using SImpl.Cache.Module;
using SImpl.Cache.Storage.Memory.Module;
using SImpl.Common;

namespace SImpl.Cache.Storage.Memory
{
    public static class BuilderExtensions
    {
        public static CacheModuleConfig AddMemoryCacheStorage(this CacheModuleConfig cacheModuleConfig, Action<CacheLayerOptions> configureDelegate)
        {
            cacheModuleConfig.AddCacheLayer(() => new MemoryCacheModule(new CacheLayerDefinition()), definition =>
            {
                definition.CacheServiceType = TypeOf.New<ICacheService, IMemoryCacheService>();
                configureDelegate?.Invoke(definition.Options);
            });
            return cacheModuleConfig;
        }
    }
}
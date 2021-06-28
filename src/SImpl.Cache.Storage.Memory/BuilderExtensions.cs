using System;
using SImpl.Cache.Models;
using SImpl.Cache.Module;
using SImpl.Cache.Storage.Memory.Module;

namespace SImpl.Cache.Storage.Memory
{
    public static class BuilderExtensions
    {
        public static CacheModuleConfig AddMemoryCacheStorage(this CacheModuleConfig cacheModuleConfig, Action<CacheLayerDefinition> configure)
        {
            cacheModuleConfig.AddCacheLayer(() => new MemoryCacheModule(new CacheLayerDefinition()), configure);
            return cacheModuleConfig;
        }
    }
}
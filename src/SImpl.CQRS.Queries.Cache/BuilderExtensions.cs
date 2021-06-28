using System;
using SImpl.Cache.Module;
using SImpl.CQRS.Queries.Cache.Module;

namespace SImpl.CQRS.Queries.Cache
{
    public static class BuilderExtensions
    {
        public static CacheModuleConfig AddQueryCache(this CacheModuleConfig cacheModuleConfig, Action<QueryCacheModuleConfig> configureDelegate = null)
        {
            var module = cacheModuleConfig.AttachCacheExtension(() => new QueryCacheModule(new QueryCacheModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return cacheModuleConfig;
        }
    }
}
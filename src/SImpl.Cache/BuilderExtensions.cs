using System;
using SImpl.Cache.Module;
using SImpl.Host.Builders;

namespace SImpl.Cache
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseCache(this ISImplHostBuilder host, Action<CacheModuleConfig> configureDelegate = null)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new CacheModule(new CacheModuleConfig(host)));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}
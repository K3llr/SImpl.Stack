using System;
using SImpl.Host.Builders;
using SImpl.Storage.Redis.Module;

namespace SImpl.Storage.Redis
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseRedis(this ISImplHostBuilder host, Action<RedisConfig> configureDelegate)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new RedisModule(new RedisConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}
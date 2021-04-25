using System;
using SImpl.Host.Builders;
using SImpl.Queue.Module;

namespace SImpl.Queue
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseDependencyInjection(this ISImplHostBuilder host, Action<DependencyInjectionConfig> queueConfig)
        {
            var config = new DependencyInjectionConfig();
            queueConfig.Invoke(config);
            
            var module = host.AttachNewOrGetConfiguredModule(() => new DependencyInjectionModule(config));
            return host;
        }
    }
}
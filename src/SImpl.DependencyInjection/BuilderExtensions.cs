using System;
using SImpl.DependencyInjection.Module;
using SImpl.Host.Builders;

namespace SImpl.DependencyInjection
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
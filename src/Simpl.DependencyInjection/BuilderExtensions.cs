using System;
using Simpl.DependencyInjection.Module;
using SImpl.Host.Builders;

namespace Simpl.DependencyInjection
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
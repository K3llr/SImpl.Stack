using System;
using SImpl.CQRS.Events.Module;
using SImpl.Host.Builders;

namespace SImpl.CQRS.Events
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseCqrsInMemoryEventDispatcher(this ISImplHostBuilder host)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new EventModule(new EventModuleConfig()));
            module.Config.EnableInMemoryEventDispatcher = true;
            return host;
        }

        public static ISImplHostBuilder UseCqrsEvents(this ISImplHostBuilder host, Action<EventModuleConfig> configureDelegate = null)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new EventModule(new EventModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}
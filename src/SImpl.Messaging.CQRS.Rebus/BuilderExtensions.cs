using System;
using SImpl.Host.Builders;
using SImpl.Messaging.CQRS.Rebus.Module;

namespace SImpl.Messaging.CQRS.Rebus
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseCqrsMessagingCommandDispatcher(this ISImplHostBuilder host, Action<MessagingCqrsModuleConfig> configureDelegate)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new MessagingCqrsModule(new MessagingCqrsModuleConfig()));
            configureDelegate?.Invoke(module.Config);
            
            return host;
        }
    }
}
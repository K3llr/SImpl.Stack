using System;
using SImpl.Host.Builders;
using SImpl.Messaging.CQRS.Module;

namespace SImpl.Messaging.CQRS
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
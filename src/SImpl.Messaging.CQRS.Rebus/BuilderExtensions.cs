using System;
using Rebus.Config;
using SImpl.Host.Builders;
using SImpl.Messaging.CQRS.Rebus.Module;

namespace SImpl.Messaging.CQRS.Rebus
{
    public static class BuilderExtensions
    {
        [Obsolete("Use UseCqrsMessaging")]
        public static ISImplHostBuilder UseCqrsMessagingCommandDispatcher(this ISImplHostBuilder host, Action<MessagingCqrsModuleConfig> configureDelegate)
        {
            return UseCqrsMessagingDispatchers(host, configureDelegate);
        }

        private static ISImplHostBuilder UseCqrsMessagingDispatchers(ISImplHostBuilder host, Action<MessagingCqrsModuleConfig> configureDelegate)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new MessagingCqrsModule(new MessagingCqrsModuleConfig()));
            module.Config.EnableMessagingCommandDispatcher = true;
            module.Config.EnableMessagingEventDispatcher = true;
            configureDelegate?.Invoke(module.Config);

            return host;
        }

        public static ISImplHostBuilder UseCqrsMessaging(this ISImplHostBuilder host,
            Action<MessagingCqrsModuleConfig> configureDelegate,
            Func<RebusConfigurer, RebusConfigurer> configureRebusService = null)
        {
            var module = host.AttachNewOrGetConfiguredModule(() => new MessagingCqrsModule(
                    new MessagingCqrsModuleConfig(configureRebusService)
                )
            );
            configureDelegate?.Invoke(module.Config);

            return host;
        }
    }
}
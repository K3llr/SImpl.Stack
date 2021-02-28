using SImpl.Host.Builders;
using SImpl.Messaging.CQRS.Module;

namespace SImpl.Messaging.CQRS
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseCqrsMessagingCommandDispatcher(this ISImplHostBuilder host)
        {
            host.AttachNewOrGetConfiguredModule(() => new MessagingCqrsModule());
            return host;
        }
    }
}
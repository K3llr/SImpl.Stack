using Microsoft.Extensions.DependencyInjection;
using SImpl.CQRS.Commands;
using SImpl.Messaging.CQRS.Services;
using SImpl.Modules;

namespace SImpl.Messaging.CQRS.Module
{
    public class MessagingCqrsModule : IServicesCollectionConfigureModule
    {
        public string Name => nameof(MessagingCqrsModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, RebusCommandDispatcher>();
            services.AddSingleton<IMessagingCommandDispatcher, RebusCommandDispatcher>();
        }
    }
}
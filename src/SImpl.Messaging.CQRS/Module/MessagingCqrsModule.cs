using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Handlers;
using SImpl.CQRS.Commands;
using SImpl.Messaging.CQRS.Services;
using SImpl.Modules;

namespace SImpl.Messaging.CQRS.Module
{
    public class MessagingCqrsModule : IServicesCollectionConfigureModule
    {
        public MessagingCqrsModuleConfig Config { get; }
        
        public string Name => nameof(MessagingCqrsModule);

        public MessagingCqrsModule(MessagingCqrsModuleConfig config)
        {
            Config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, RebusCommandDispatcher>();
            services.AddSingleton<IMessagingCommandDispatcher, RebusCommandDispatcher>();
            
            var cmdTypes = Config.RegisteredAssemblies.SelectMany(s => s.GetTypes())
                .Where(p => p.IsAssignableTo(typeof(ICommand)));

            var genericInterface = typeof(IHandleMessages<>);
            var genericImpl = typeof(CommandMessageHandler<>);
                
            foreach (var cmdType in cmdTypes)
            {
                services.AddSingleton(genericInterface.MakeGenericType(cmdType), genericImpl.MakeGenericType(cmdType));
            }
        }
    }
}
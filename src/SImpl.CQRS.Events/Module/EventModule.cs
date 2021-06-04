using Microsoft.Extensions.DependencyInjection;
using SImpl.CQRS.Events.Services;
using SImpl.Modules;

namespace SImpl.CQRS.Events.Module
{
    public class EventModule : IServicesCollectionConfigureModule
    {
        public string Name => nameof(EventModule);
        public EventModuleConfig Config { get; }

        public EventModule(EventModuleConfig config)
        {
            Config = config;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            if (Config.EnableInMemoryEventDispatcher)
            {
                services.AddSingleton<IEventDispatcher, InMemoryEventDispatcher>();
                services.AddSingleton<IInMemoryEventDispatcher, InMemoryEventDispatcher>();
            }
            
            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            foreach (var commandHandlerType in Config.RegisteredEventHandlers)
            {
                // TODO:
            }
        }
    }
}
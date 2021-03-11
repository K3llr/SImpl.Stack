using Microsoft.Extensions.DependencyInjection;
using SImpl.JobQueues;
using SImpl.JobQueues.Services;
using SImpl.Modules;

namespace SImpl.CQRS.Commands.Module
{
    public class QueueModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(QueueModule);
        public void ConfigureServices(IServiceCollection services)
        {
            if (Config.EnableInMemoryQueueManager)
            {
                services.AddSingleton<IQueueManager, InMemoryQueueManager>();
                services.AddSingleton<IInMemoryQueueManager, InMemoryQueueManager>();
            }
            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IQueue<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IDeQueueAction<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            foreach (var queryHandlerType in Config.RegisteredQueues)
            {
                // TODO:
            }
        }

        public QueueModuleConfig Config { get; }
        
        public QueueModule(QueueModuleConfig config)
        {
            Config = config;
        }
    }
}
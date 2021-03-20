using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Queue.Services;

namespace SImpl.Queue.Module
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
                    .AddClasses(c => c.AssignableTo(typeof(IDequeueAction<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            
            foreach (var queryHandlerType in Config.RegisteredQueues)
            {
                // TODO: add queue registration by type
            }
        }

        public QueueModuleConfig Config { get; }
        
        public QueueModule(QueueModuleConfig config)
        {
            Config = config;
        }
    }
}
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
            var genericInterface = typeof(IQueue<>);
            var genericImpl = typeof(IDequeueAction<>);
            foreach (var queue in Config.RegisteredQueues)
            {
                services.AddSingleton(genericInterface.MakeGenericType(queue));
                
            }
            foreach (var dequeueAction in Config.RegisteredDequeueActions)
            {
                services.AddSingleton(genericImpl.MakeGenericType(dequeueAction));
            }
            
        }

        public QueueModuleConfig Config { get; }
        
        public QueueModule(QueueModuleConfig config)
        {
            Config = config;
        }
    }
}
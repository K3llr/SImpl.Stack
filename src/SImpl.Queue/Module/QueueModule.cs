using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Queue.Services;

namespace SImpl.Queue.Module
{
    public class QueueModule : IServicesCollectionConfigureModule
    {
        public QueueModuleConfig Config { get; }
        
        public QueueModule(QueueModuleConfig config)
        {
            Config = config;
        }
        
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
                    .WithSingletonLifetime());
            
            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IDequeueAction<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
            
            var queueInterface = typeof(IQueue<>);
            foreach (var queueReg in Config.RegisteredQueues)
            {
                services.AddSingleton(queueInterface.MakeGenericType(queueReg.ItemType), queueReg.QueueType);
            }
            
            var dequeueInterface = typeof(IDequeueAction<>);
            foreach (var dequeueReg in Config.RegisteredDequeueActions)
            {
                services.AddSingleton(dequeueInterface.MakeGenericType(dequeueReg.ItemType), dequeueReg.QueueType);
            }
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.Queue.Services;

namespace SImpl.Queue.Module
{
    public class QueueModule(QueueModuleConfig config) : IServicesCollectionConfigureModule
    {
        public QueueModuleConfig Config { get; } = config;

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
                    .AddClasses(c => c.AssignableTo(typeof(IQueue<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            
            services.Scan(s =>
                s.FromAssemblies(Config.RegisteredAssemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IDequeueAction<>)), publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
            
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
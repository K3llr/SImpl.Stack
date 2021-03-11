using Microsoft.Extensions.DependencyInjection;
using SImpl.JobQueues;
using SImpl.Modules;

namespace SImpl.CQRS.Commands.Module
{
    public class QueueModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(QueueModule);
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IQueue<>),Config.RegisteredQueues);
        }

        public QueueModuleConfig Config { get; }
        
        public QueueModule(QueueModuleConfig config)
        {
            Config = config;
        }
    }
}
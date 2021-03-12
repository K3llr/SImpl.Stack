using System;
using SImpl.Host.Builders;
using SImpl.Queue.Module;

namespace SImpl.Queue
{
    public static class BuilderExtensions
    {
        public static ISImplHostBuilder UseInMemoryQueues(this ISImplHostBuilder host, Action<QueueModuleConfig> queueConfig)
        {
            var config = new QueueModuleConfig();
            queueConfig.Invoke(config);
            
            var module = host.AttachNewOrGetConfiguredModule(() => new QueueModule(config));
            module.Config.EnableInMemoryQueueManager = true;
            
            return host;
        }
    }
}
using System;
using SImpl.CQRS.Commands.Module;
using SImpl.Host.Builders;

namespace SImpl.CQRS.Queries
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
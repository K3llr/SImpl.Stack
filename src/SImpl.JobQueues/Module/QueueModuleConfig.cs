using System;
using System.Collections.Generic;
using System.Reflection;
using SImpl.JobQueues;

namespace SImpl.CQRS.Commands.Module
{
    public class QueueModuleConfig
    {
        private readonly List<IQueue> _queues = new();
        
        public IReadOnlyList<IQueue> RegisteredQueues => _queues.AsReadOnly();
        public bool EnableInMemoryQueueManager { get; internal set; }

        public QueueModuleConfig AddQueue<T>(IQueue<T> queue)
        {
            _queues.Add(queue);
            return this;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.Queue.Module
{
    public class QueueModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<QueueRegistration> _queues = new();
        private readonly List<QueueRegistration> _dequeueActions = new();
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<QueueRegistration> RegisteredQueues => _queues.AsReadOnly();
        public IReadOnlyList<QueueRegistration> RegisteredDequeueActions  => _dequeueActions.AsReadOnly();
        
        public bool EnableInMemoryQueueManager { get; internal set; }
        
        public QueueModuleConfig AddQueuesAndDequeueActionsFromAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }
        
        public QueueModuleConfig AddQueuesAndDequeueActionsFromAssemblyOf<T>()
        {
            AddQueuesAndDequeueActionsFromAssembly(typeof(T).Assembly);
            return this;
        }
        
        public QueueModuleConfig AddQueue<TQueue, T>()
            where TQueue : IQueue<T>
        {
            _queues.Add(new QueueRegistration
            {
                QueueType = typeof(TQueue),
                ItemType = typeof(T)
            });
            return this;
        }
        
        public QueueModuleConfig AddDequeueAction<TDequeueAction, T>()
            where TDequeueAction : IDequeueAction<T>
        {
            _queues.Add(new QueueRegistration
            {
                QueueType = typeof(TDequeueAction),
                ItemType = typeof(T)
            });
            return this;
        }
    }
    
    public class QueueRegistration
    {
        public Type QueueType { get; set; }
        public Type ItemType { get; set; }
    }
}
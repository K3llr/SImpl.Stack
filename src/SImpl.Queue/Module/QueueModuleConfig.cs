using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.Queue.Module
{
    public class QueueModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<Type> _queues = new();
        private readonly List<Type> _dequeueActions = new();
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<Type> RegisteredQueues => _queues.AsReadOnly();
        public IReadOnlyList<Type> RegisteredDequeueActions  => _queues.AsReadOnly();
        
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
    }
}
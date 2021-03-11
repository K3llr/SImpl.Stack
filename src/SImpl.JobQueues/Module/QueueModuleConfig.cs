using System;
using System.Collections.Generic;
using System.Reflection;
using SImpl.JobQueues;

namespace SImpl.CQRS.Commands.Module
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
        public QueueModuleConfig AddQueuesAndDequeuActionsFromAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }
        
        public QueueModuleConfig AddQueuesAndDequeuActionsFromAssemblyOf<T>()
        {
            AddQueuesAndDequeuActionsFromAssembly(typeof(T).Assembly);
            return this;
        }
    }
}
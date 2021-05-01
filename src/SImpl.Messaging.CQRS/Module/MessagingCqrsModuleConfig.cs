using System.Collections.Generic;
using System.Reflection;

namespace SImpl.Messaging.CQRS.Module
{
    public class MessagingCqrsModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        
        public MessagingCqrsModuleConfig AddMessageHandlersForCommandsFromAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }
        
        public MessagingCqrsModuleConfig AddMessageHandlersForCommandsFromAssemblyOf<T>()
        {
            AddMessageHandlersForCommandsFromAssembly(typeof(T).Assembly);
            return this;
        }
    }
}
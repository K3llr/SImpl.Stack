using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.CQRS.Events.Module
{
    public class EventModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<Type> _eventHandlers = new();
        
        public bool EnableInMemoryEventDispatcher { get; set; }
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<Type> RegisteredEventHandlers => _eventHandlers.AsReadOnly();
        
        public EventModuleConfig AddEventHandlersFromAssembly(Assembly assembly)
        {
            _assemblies.Add(assembly);
            return this;
        }
        
        public EventModuleConfig AddEventHandlersFromAssembly<T>()
        {
            AddEventHandlersFromAssembly(typeof(T).Assembly);
            return this;
        }
        
        // TODO:
        /*public EventModuleConfig AddCommandHandler<TEventHandler, TEvent>()
            where TEventHandler : IEventHandler<TEvent> 
            where TEvent : class, IEvent
        {
            _eventHandlers.Add(typeof(TEventHandler));
            return this;
        }*/
    }
}
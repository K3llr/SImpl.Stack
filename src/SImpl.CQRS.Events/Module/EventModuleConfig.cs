using System;
using System.Collections.Generic;
using System.Reflection;

namespace SImpl.CQRS.Events.Module
{
    public class EventModuleConfig
    {
        private readonly List<Assembly> _assemblies = new();
        private readonly List<EventHandlerRegistration> _eventHandlers = new();
        
        public bool EnableInMemoryEventDispatcher { get; set; }
        
        public IReadOnlyList<Assembly> RegisteredAssemblies => _assemblies.AsReadOnly();
        public IReadOnlyList<EventHandlerRegistration> RegisteredEventHandlers => _eventHandlers.AsReadOnly();
        
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

        public EventModuleConfig AddEventHandler<TEventHandler, TEvent>()
            where TEventHandler : IEventHandler<TEvent> 
            where TEvent : class, IEvent
        {
            _eventHandlers.Add(new EventHandlerRegistration
            {
                EventType = typeof(TEvent),
                EventHandlerType = typeof(TEventHandler)
            });
            return this;
        }
    }
    
    public class EventHandlerRegistration
    {
        public Type EventType { get; set; }
        public Type EventHandlerType { get; set; }
    }
}
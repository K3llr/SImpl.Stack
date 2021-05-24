using System;
using System.Collections.Generic;
using System.Reflection;
using Rebus.Bus;

namespace SImpl.Messaging.CQRS.Module
{
    public class MessagingCqrsModuleConfig
    {
        private readonly List<Assembly> _cmdAssemblies = new();
        private readonly List<Assembly> _eventAssemblies = new();
        
        public IReadOnlyList<Assembly> RegisteredCommandAssemblies => _cmdAssemblies.AsReadOnly();
        public IReadOnlyList<Assembly> RegisteredEventAssemblies => _eventAssemblies.AsReadOnly();

        public bool InitBusOnStartEnabled { get; set; } = false;
        public MessagingCqrsModuleConfig InitBusOnStart(bool initBusOnStart = false)
        {
            InitBusOnStartEnabled = initBusOnStart;
            return this;
        }

        public bool SubscribeToEventsOnStartEnabled { get; set; } = false;
        public MessagingCqrsModuleConfig SubscribeToEventsOnStart(bool subscribeToEventsOnStart = false)
        {
            SubscribeToEventsOnStartEnabled = subscribeToEventsOnStart;
            return this;
        }

        public Action<IBus> BusConfigureDelegate { get; set; } = bus => { }; 
        public MessagingCqrsModuleConfig ConfigureBusOnStart(Action<IBus> busConfigureDelegate)
        {
            BusConfigureDelegate = busConfigureDelegate;
            return this;
        }
        
        public MessagingCqrsModuleConfig HandleCommandsFromAssemblyOf(Assembly assembly)
        {
            _cmdAssemblies.Add(assembly);
            return this;
        }
        
        public MessagingCqrsModuleConfig HandleCommandsFromAssemblyOf<T>()
        {
            HandleCommandsFromAssemblyOf(typeof(T).Assembly);
            return this;
        }
        
        public MessagingCqrsModuleConfig SubscribeToEventsFromAssembly(Assembly assembly)
        {
            _eventAssemblies.Add(assembly);
            return this;
        }
        
        public MessagingCqrsModuleConfig SubscribeToEventsFromAssembly<T>()
        {
            SubscribeToEventsFromAssembly(typeof(T).Assembly);
            return this;
        }
    }
}
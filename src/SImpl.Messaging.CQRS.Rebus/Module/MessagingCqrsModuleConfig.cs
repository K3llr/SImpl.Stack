using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Rebus.Bus;

namespace SImpl.Messaging.CQRS.Rebus.Module
{
    public class MessagingCqrsModuleConfig
    {
        private readonly List<Assembly> _cmdAssemblies = new();
        private readonly List<Assembly> _eventAssemblies = new();
        
        public IReadOnlyList<Assembly> RegisteredCommandAssemblies => _cmdAssemblies.AsReadOnly();
        public IReadOnlyList<Assembly> RegisteredEventAssemblies => _eventAssemblies.AsReadOnly();

        public bool InitBusOnStartEnabled { get; private set; } = false;
        public MessagingCqrsModuleConfig InitBusOnStart(bool initBusOnStart = true)
        {
            InitBusOnStartEnabled = initBusOnStart;
            return this;
        }

        public bool SubscribeToEventsOnStartEnabled { get; private set; } = false;
        public MessagingCqrsModuleConfig SubscribeToEventsOnStart(bool subscribeToEventsOnStart = true)
        {
            SubscribeToEventsOnStartEnabled = subscribeToEventsOnStart;
            return this;
        }

        public Func<IBus, Task> BusConfigureDelegate { get; private set; } = _ => Task.CompletedTask; 
        public MessagingCqrsModuleConfig ConfigureBusOnStart(Func<IBus, Task> busConfigureDelegate)
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
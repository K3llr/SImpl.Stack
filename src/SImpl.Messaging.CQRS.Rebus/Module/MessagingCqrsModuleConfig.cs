using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Rebus.Bus;
using SImpl.Common;
using SImpl.CQRS.Commands;
using SImpl.CQRS.Events;

namespace SImpl.Messaging.CQRS.Rebus.Module
{
    public class MessagingCqrsModuleConfig
    {
        private readonly List<Assembly> _cmdAssemblies = new();
        private readonly List<Type> _cmdTypes = new();
        
        public IReadOnlyList<Assembly> RegisteredCommandAssemblies => _cmdAssemblies.AsReadOnly();
        public IReadOnlyList<Type> RegisteredCommandTypes => _cmdTypes.AsReadOnly();
        
        private readonly List<Assembly> _eventAssemblies = new();
        private readonly List<Type> _eventTypes = new();
        
        public IReadOnlyList<Assembly> RegisteredEventAssemblies => _eventAssemblies.AsReadOnly();
        public IReadOnlyList<Type> RegisteredEventTypes => _eventTypes.AsReadOnly();
        
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
        
        public MessagingCqrsModuleConfig HandleCommand<TCommand>()
            where TCommand : class, ICommand
        {
            _cmdTypes.Add(typeof(TCommand));
            return this;
        }
        
        public MessagingCqrsModuleConfig HandleEventsFromAssembly(Assembly assembly)
        {
            _eventAssemblies.Add(assembly);
            return this;
        }
        
        public MessagingCqrsModuleConfig HandleEventsFromAssembly<T>()
        {
            HandleEventsFromAssembly(typeof(T).Assembly);
            return this;
        }
        
        public MessagingCqrsModuleConfig HandleEvent<TEvent>()
            where TEvent : class, IEvent
        {
            _eventTypes.Add(typeof(TEvent));
            return this;
        }
        
        public class BufferRegistration
        {
            public Type MessageType { get; set; }
            public Type BufferHandlerType { get; set; }
            public TimeSpan MaxTimeSpan { get; set; }
            public int MaxMessageCount { get; set; }
        }
        
        private readonly List<BufferRegistration> _messageHandlerBuffers = new();
        public IReadOnlyList<BufferRegistration> RegisteredMessageHandlerBuffers => _messageHandlerBuffers.AsReadOnly();
        public MessagingCqrsModuleConfig AddMessageHandlerBuffer<TMessage, TBufferHandler>(TimeSpan maxTimeSpan, int maxMessageCount)
            where TBufferHandler : IBufferHandler<TMessage>
        {
            _messageHandlerBuffers.Add(new BufferRegistration
            {
                MessageType = typeof(TMessage),
                BufferHandlerType = typeof(TBufferHandler),
                MaxTimeSpan = maxTimeSpan,
                MaxMessageCount = maxMessageCount
            });
            
            return this;
        }
        
        private readonly List<BufferRegistration> _messageDispatcherBuffers = new();
        public IReadOnlyList<BufferRegistration> RegisteredMessageDispatcherBuffers => _messageDispatcherBuffers.AsReadOnly();
        public MessagingCqrsModuleConfig AddMessageDispatcherBuffer<TCommand, TBufferHandler>(TimeSpan maxTimeSpan, int maxMessageCount)
            where TBufferHandler : IBufferHandler<TCommand>
            where TCommand : class, ICommand
        {
            _messageDispatcherBuffers.Add(new BufferRegistration
            {
                MessageType = typeof(TCommand),
                BufferHandlerType = typeof(TBufferHandler),
                MaxTimeSpan = maxTimeSpan,
                MaxMessageCount = maxMessageCount
            });
            
            return this;
        }
    }
}
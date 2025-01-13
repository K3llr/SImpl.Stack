using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.ServiceProvider;
using SImpl.CQRS.Commands;
using SImpl.CQRS.Events;
using SImpl.Hosts.WebHost.Modules;
using SImpl.Messaging.CQRS.Rebus.Services;
using SImpl.Modules;

namespace SImpl.Messaging.CQRS.Rebus.Module
{
    public class MessagingCqrsModule : IServicesCollectionConfigureModule, IAspNetPostModule
    {
        public MessagingCqrsModuleConfig Config { get; }

        public string Name => nameof(MessagingCqrsModule);
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Config.InitBusOnStartEnabled)
            {
                // Retry policy for rebus connection
                Policy
                    .Handle<global::Rebus.Injection.ResolutionException>()
                    .WaitAndRetryForever(
                        retryAttempt => TimeSpan.FromSeconds(2 * retryAttempt),
                        (exception, timespan, context) =>
                        {
                            // Add logic to be executed before each retry, such as logging
                        })
                    .Execute(() =>
                    {
                        app.ApplicationServices.UseRebus(async bus =>
                        {
                            if (Config.SubscribeToEventsOnStartEnabled)
                            {
                                var eventTypes = Config.RegisteredEventAssemblies.SelectMany(s => s.GetTypes())
                                    .Where(p => p.IsAssignableTo(typeof(IEvent)));

                                foreach (var eventType in eventTypes)
                                {
                                    await bus.Subscribe(eventType);
                                }
                            }

                            await Config.BusConfigureDelegate.Invoke(bus);
                        });
                    });
            }
        }

        public MessagingCqrsModule(MessagingCqrsModuleConfig config)
        {
            Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register commands
            if (Config.EnableMessagingCommandDispatcher)
            {
                services.AddSingleton<IMessagingCommandDispatcher, RebusCommandDispatcher>();
            }

            RegisterMessageHandlers(
                services,
                Config.RegisteredCommandAssemblies,
                Config.RegisteredCommandTypes,
                typeof(ICommand),
                typeof(IHandleMessages<>),
                typeof(CommandMessageHandler<>));

            // Register events
            if (Config.EnableMessagingEventDispatcher)
            {
                services.AddSingleton<IMessagingEventDispatcher, RebusEventDispatcher>();
            }

            RegisterMessageHandlers(
                services,
                Config.RegisteredEventAssemblies,
                Config.RegisteredEventTypes,
                typeof(IEvent),
                typeof(IHandleMessages<>),
                typeof(EventMessageHandler<>));

            // Register messageHandler buffer
            var handlerBufferConfigType = typeof(HandlerBufferConfig<>);
            var messageHandlerType = typeof(IHandleMessages<>);
            var bufferMessageHandlerType = typeof(BufferedMessageHandler<>);
            var bufferHandlerType = typeof(IBufferHandler<>);
            var registeredBufferHandlers = new List<Type>();

            foreach (var messagesBuffer in Config.RegisteredMessageHandlerBuffers)
            {
                // Register config
                var concreteBufferConfigType = handlerBufferConfigType.MakeGenericType(messagesBuffer.MessageType);
                var config = Activator.CreateInstance(concreteBufferConfigType, messagesBuffer.MaxTimeSpan, messagesBuffer.MaxMessageCount);
                services.AddSingleton(concreteBufferConfigType, config);

                // Register handler
                var genericMessageHandler = messageHandlerType.MakeGenericType(messagesBuffer.MessageType);
                var genericBufferMessageHandler = bufferMessageHandlerType.MakeGenericType(messagesBuffer.MessageType);
                services.AddSingleton(genericMessageHandler, genericBufferMessageHandler);

                // Register buffer handler
                var genericBufferHandlerType = bufferHandlerType.MakeGenericType(messagesBuffer.MessageType);
                if (registeredBufferHandlers.All(type => type != genericBufferHandlerType))
                {
                    registeredBufferHandlers.Add(genericBufferHandlerType);
                    services.AddSingleton(genericBufferHandlerType, messagesBuffer.BufferHandlerType);
                }
            }

            // Register commandDispatch buffer
            var dispatchBufferConfigType = typeof(DispatchBufferConfig<>);
            var dispatcherType = typeof(ICommandBufferDispatcher<>);
            var commandBufferDispatcherType = typeof(CommandBufferDispatcher<>);

            foreach (var messagesBuffer in Config.RegisteredMessageDispatcherBuffers)
            {
                // Register config
                var concreteBufferConfigType = dispatchBufferConfigType.MakeGenericType(messagesBuffer.MessageType);
                var config = Activator.CreateInstance(concreteBufferConfigType, messagesBuffer.MaxTimeSpan, messagesBuffer.MaxMessageCount);
                services.AddSingleton(concreteBufferConfigType, config);

                // Register dispatcher
                var genericDispatcher = dispatcherType.MakeGenericType(messagesBuffer.MessageType);
                var genericCommandBufferDispatcher = commandBufferDispatcherType.MakeGenericType(messagesBuffer.MessageType);
                services.AddSingleton(genericDispatcher, genericCommandBufferDispatcher);

                // Register buffer handler
                var genericBufferHandlerType = bufferHandlerType.MakeGenericType(messagesBuffer.MessageType);
                if (registeredBufferHandlers.All(type => type != genericBufferHandlerType))
                {
                    registeredBufferHandlers.Add(genericBufferHandlerType);
                    services.AddSingleton(genericBufferHandlerType, messagesBuffer.BufferHandlerType);
                }
            }
        }

        private void RegisterMessageHandlers(
            IServiceCollection services,
            IReadOnlyList<Assembly> assemblies,
            IReadOnlyList<Type> types,
            Type lookForType,
            Type genericInterface,
            Type genericImpl)
        {
            var allTypes = assemblies.SelectMany(s => s.GetTypes())
                .Where(p => p.IsAssignableTo(lookForType) && p.IsAbstract == false)
                .Union(types)
                .Distinct();

            foreach (var type in allTypes)
            {
                services.AddSingleton(genericInterface.MakeGenericType(type), genericImpl.MakeGenericType(type));
            }
        }
    }
}
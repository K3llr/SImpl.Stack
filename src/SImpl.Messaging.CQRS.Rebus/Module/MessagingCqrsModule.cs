using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Polly;
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
            services.AddSingleton<IMessagingCommandDispatcher, RebusCommandDispatcher>();
            
            RegisterMessageHandlers(
                services, 
                Config.RegisteredCommandAssemblies, 
                Config.RegisteredCommandTypes,
                typeof(ICommand), 
                typeof(IHandleMessages<>), 
                typeof(CommandMessageHandler<>));

            // Register events
            services.AddSingleton<IMessagingEventDispatcher, RebusEventDispatcher>();
            
            RegisterMessageHandlers(
                services, 
                Config.RegisteredEventAssemblies, 
                Config.RegisteredEventTypes, 
                typeof(IEvent), 
                typeof(IHandleMessages<>), 
                typeof(EventMessageHandler<>));

            // Register buffered messageHandler
            var bufferConfigType = typeof(MessageBufferConfig<>);
            var messageHandlerType = typeof(IHandleMessages<>);
            var bufferMessageHandlerType = typeof(BufferedMessageHandler<>);
            var bufferHandlerType = typeof(IBufferHandler<>);
            
            foreach (var messagesBuffer in Config.RegisteredMessagesBuffers)
            {
                var concreteBufferConfigType = bufferConfigType.MakeGenericType(messagesBuffer.MessageType);
                var config = Activator.CreateInstance(concreteBufferConfigType, messagesBuffer.MaxTimeSpan, messagesBuffer.MaxMessageCount);
                services.AddSingleton(concreteBufferConfigType, config);
                
                var genericMessageHandler = messageHandlerType.MakeGenericType(messagesBuffer.MessageType);
                var genericBufferMessageHandler = bufferMessageHandlerType.MakeGenericType(messagesBuffer.MessageType);
                services.AddSingleton(genericMessageHandler, genericBufferMessageHandler);

                var genericBufferHandlerType = bufferHandlerType.MakeGenericType(messagesBuffer.MessageType);
                services.AddSingleton(genericBufferHandlerType, messagesBuffer.BufferHandlerType);
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
                .Where(p => p.IsAssignableTo(lookForType))
                .Union(types)
                .Distinct();

            foreach (var type in allTypes)
            {
                services.AddSingleton(genericInterface.MakeGenericType(type), genericImpl.MakeGenericType(type));
            }
        }
    }
}
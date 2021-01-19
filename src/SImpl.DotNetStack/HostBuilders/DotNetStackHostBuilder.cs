using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Exceptions;
using SImpl.DotNetStack.Host;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.HostBuilders
{
    public class DotNetStackHostBuilder : IDotNetStackHostBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IHostBootManager _bootManager;

        public DotNetStackHostBuilder(IDotNetStackRuntime runtime, IModuleManager moduleManager, IHostBootManager bootManager)
        {
            Runtime = runtime;
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

        public IDotNetStackRuntime Runtime { get; }

        public void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var module = _moduleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                _moduleManager.AttachModule(factory.Invoke());
            }
            else
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var module = _moduleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                module = factory.Invoke();
                _moduleManager.AttachModule(module);
            }

            return module;
        }

        public void Configure(IDotNetStackHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(hostBuilder);
            
            _bootManager.PreInit();
            _bootManager.ConfigureServices(hostBuilder);
            _bootManager.ConfigureHostBuilder(hostBuilder);
        }

        public IHost Build()
        {
            var innerHost = Runtime.HostBuilder.Build();
            
            _bootManager.ConfigureHost(innerHost);
            
            return new DotNetStackHost(innerHost, _bootManager);
        }
        
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            Runtime.HostBuilder.ConfigureHostConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            Runtime.HostBuilder.ConfigureAppConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            Runtime.HostBuilder.ConfigureServices(configureDelegate);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            Runtime.HostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            Runtime.HostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            Runtime.HostBuilder.ConfigureContainer(configureDelegate);
            return this;
        }

        public IDictionary<object, object> Properties => Runtime.HostBuilder.Properties;
    }
}
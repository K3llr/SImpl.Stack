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

        public IHost Build()
        {
            _bootManager.PreInit();
            
            _bootManager.ConfigureServices(this);
            
            _bootManager.ConfigureHostBuilder(this);

            // Build the inner host
            var innerHost = Runtime.HostBuilder.Build();
            
            _bootManager.ConfigureHost(innerHost);
            
            _moduleManager.SetModuleState(ModuleState.Configured);
            
            return new DotNetStackHost(innerHost, _moduleManager);
        }
        
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureHostConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureAppConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureServices(configureDelegate);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            return Runtime.HostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            return Runtime.HostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureContainer(configureDelegate);
        }

        public IDictionary<object, object> Properties => Runtime.HostBuilder.Properties;
    }
}
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Exceptions;
using SImpl.DotNetStack.Runtime.Host;

namespace SImpl.DotNetStack.Runtime.HostBuilders
{
    public class DotNetStackHostBuilder : IDotNetStackHostBuilder
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly IModuleManager _moduleManager;
        private readonly IHostBootManager _bootManager;

        public DotNetStackHostBuilder(IHostBuilder hostBuilder, IModuleManager moduleManager, IHostBootManager bootManager)
        {
            _hostBuilder = hostBuilder;
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

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
            var innerHost = _hostBuilder.Build();
            
            _bootManager.ConfigureHost(innerHost);
            
            return new DotNetStackHost(innerHost, _bootManager);
        }
        
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureHostConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureAppConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            _hostBuilder.ConfigureServices(configureDelegate);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            _hostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            _hostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureContainer(configureDelegate);
            return this;
        }

        public IDictionary<object, object> Properties => _hostBuilder.Properties;
    }
}
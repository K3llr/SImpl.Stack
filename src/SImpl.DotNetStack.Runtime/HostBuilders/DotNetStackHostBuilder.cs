using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;
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
            _moduleManager.AttachModule(factory.Invoke());
        }

        public TModule GetConfiguredModule<TModule>() where TModule : IDotNetStackModule
        {
            return _moduleManager.GetConfiguredModule<TModule>();
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(IDotNetStackHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> configureDelegate)
        {
            // Configure the stack host (aka attach all stack host modules) 
            configureDelegate?.Invoke(hostBuilder);
            
            // Initialize all stack host modules
            // If host app is configures, PreInit will attach application modules
            _bootManager.PreInit();
            
            // All modules (host and application) has been attached
            // Modules register services
            _bootManager.ConfigureServices(hostBuilder);
            
            // Modules configure the host builder
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
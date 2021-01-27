using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Host.Builders
{
    public class SImplHostBuilder : ISImplHostBuilder
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly IModuleManager _moduleManager;
        private readonly IHostBootManager _bootManager;

        public SImplHostBuilder(IHostBuilder hostBuilder, IModuleManager moduleManager, IHostBootManager bootManager)
        {
            _hostBuilder = hostBuilder;
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

        public ISImplHostBuilder Use<TModule>(Func<TModule> factory)
            where TModule : ISImplModule
        {
            _moduleManager.AttachModule(factory.Invoke());
            return this;
        }

        public TModule GetConfiguredModule<TModule>() where TModule : ISImplModule
        {
            return _moduleManager.GetConfiguredModule<TModule>();
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : ISImplModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(ISImplHostBuilder hostBuilder, Action<ISImplHostBuilder> configureDelegate)
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
            
            return new SImplHost(innerHost, _bootManager);
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
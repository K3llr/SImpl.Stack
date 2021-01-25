using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Stack.Diagnostics;
using SImpl.Stack.HostBuilders;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Runtime.Diagnostics
{
    public class DiagnosticsHostBuilder : IDotNetStackHostBuilder
    {
        private readonly IDotNetStackHostBuilder _hostBuilder;
        private readonly IModuleManager _moduleManager;
        private readonly IBootSequenceFactory _bootSequenceFactory;
        private readonly IDiagnosticsCollector _diagnostics;
        private readonly RuntimeFlags _runtimeFlags;
        private readonly ILogger<DiagnosticsHost> _logger;

        public DiagnosticsHostBuilder(IDotNetStackHostBuilder hostBuilder, IModuleManager moduleManager, IBootSequenceFactory bootSequenceFactory, IDiagnosticsCollector diagnostics, RuntimeFlags runtimeFlags, ILogger<DiagnosticsHost> logger)
        {
            _hostBuilder = hostBuilder;
            _moduleManager = moduleManager;
            _bootSequenceFactory = bootSequenceFactory;
            _diagnostics = diagnostics;
            _runtimeFlags = runtimeFlags;
            _logger = logger;
            
            _diagnostics.RegisterLapTime("HostBuilder created");
        }
        
        public IHost Build()
        {
            _diagnostics.RegisterLapTime("Host building");
            var host = new DiagnosticsHost(_hostBuilder.Build(), _moduleManager, _bootSequenceFactory, _diagnostics, _runtimeFlags, _logger);
            _diagnostics.RegisterLapTime("Host built");
            
            return host;
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureAppConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureContainer(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureHostConfiguration(configureDelegate);
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

        public IDictionary<object, object> Properties => _hostBuilder.Properties;
        
        public void Use<TModule>(Func<TModule> factory) 
            where TModule : IDotNetStackModule
        {
            _hostBuilder.Use(factory);
        }

        public TModule GetConfiguredModule<TModule>()
            where TModule : IDotNetStackModule
        {
            return _hostBuilder.GetConfiguredModule<TModule>();
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) 
            where TModule : IDotNetStackModule
        {
            return _hostBuilder.AttachNewOrGetConfiguredModule(factory);
        }

        public void Configure(IDotNetStackHostBuilder hostBuilder, Action<IDotNetStackHostBuilder> configureDelegate)
        {
            _hostBuilder.Configure(hostBuilder, configureDelegate);
        }
    }
}
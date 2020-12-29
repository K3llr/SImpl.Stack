using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Diagnostics
{
    public class DiagnosticsHostBuilder : IDotNetStackHostBuilder
    {
        private readonly IDotNetStackHostBuilder _hostBuilder;
        private readonly IDotNetStackRuntime _runtime;
        private readonly IDiagnosticsCollector _diagnostics;
        private readonly ILogger<DiagnosticsHost> _logger;

        public DiagnosticsHostBuilder(IDotNetStackHostBuilder hostBuilder, IDotNetStackRuntime runtime, IDiagnosticsCollector diagnostics, ILogger<DiagnosticsHost> logger)
        {
            _hostBuilder = hostBuilder;
            _runtime = runtime;
            _diagnostics = diagnostics;
            _logger = logger;
            
            _diagnostics.RegisterLapTime("HostBuilder created");
        }
        
        public IHost Build()
        {
            _diagnostics.RegisterLapTime("Host building");
            var host = new DiagnosticsHost(_hostBuilder.Build(), _runtime, _diagnostics, _logger);
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
        
        public IDotNetStackRuntime Runtime => _hostBuilder.Runtime;
        
        public void Use<TModule>(Func<TModule> factory) where TModule : IDotNetStackModule
        {
            _hostBuilder.Use(factory);
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) where TModule : IDotNetStackModule
        {
            return _hostBuilder.AttachNewOrGetConfiguredModule(factory);
        }
    }
}
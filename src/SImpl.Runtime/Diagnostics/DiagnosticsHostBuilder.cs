using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Host.Builders;
using SImpl.Modules;
using SImpl.Runtime.Core;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime.Diagnostics
{
    public class DiagnosticsHostBuilder : ISImplHostBuilder
    {
        private readonly ISImplHostBuilder _hostBuilder;
        private readonly IModuleManager _moduleManager;
        private readonly IBootSequenceFactory _bootSequenceFactory;
        private readonly IDiagnosticsCollector _diagnostics;
        private readonly RuntimeFlags _runtimeFlags;
        private readonly ILogger<DiagnosticsHost> _logger;

        public DiagnosticsHostBuilder(ISImplHostBuilder hostBuilder, IModuleManager moduleManager, IBootSequenceFactory bootSequenceFactory, IDiagnosticsCollector diagnostics, RuntimeFlags runtimeFlags, ILogger<DiagnosticsHost> logger)
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

        public ISImplHostBuilder Use(ISImplModule module)
        {
            _hostBuilder.Use(module);
            return this;
        }

        public ISImplHostBuilder Use<TModule>(Func<TModule> factory) 
            where TModule : ISImplModule
        {
            _hostBuilder.Use(factory);
            return this;
        }

        public TModule GetConfiguredModule<TModule>()
            where TModule : ISImplModule
        {
            return _hostBuilder.GetConfiguredModule<TModule>();
        }

        public TModule GetConfiguredModule<TModule>(Type moduleType) 
            where TModule : ISImplModule
        {
            return _hostBuilder.GetConfiguredModule<TModule>(moduleType);
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) 
            where TModule : ISImplModule
        {
            return _hostBuilder.AttachNewOrGetConfiguredModule(factory);
        }

        public void Configure(ISImplHostBuilder hostBuilder, Action<ISImplHostBuilder> configureDelegate)
        {
            _hostBuilder.Configure(hostBuilder, configureDelegate);
        }
    }
}
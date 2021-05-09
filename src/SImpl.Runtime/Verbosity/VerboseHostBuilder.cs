using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Host.Builders;
using SImpl.Modules;

namespace SImpl.Runtime.Verbosity
{
    public class VerboseHostBuilder : ISImplHostBuilder
    {
        private readonly ISImplHostBuilder _hostBuilder;
        private readonly ILogger _logger;

        public VerboseHostBuilder(ISImplHostBuilder hostBuilder, ILogger logger)
        {
            _hostBuilder = hostBuilder;
            _logger = logger;
           
            _logger.LogInformation("Verbosity is ON");
        }

        public IHost Build()
        {
            _logger.LogDebug("HostBuilder > Building host > started");
            var host = new VerboseHost(_hostBuilder.Build(), _logger);
            _logger.LogDebug("HostBuilder > Building host > ended");
            
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
            _logger.LogDebug("HostBuilder > ConfigureServices > started");
            _hostBuilder.ConfigureServices(configureDelegate);
            _logger.LogDebug("HostBuilder > ConfigureServices > ended");
            
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
            _logger.LogDebug("HostBuilder > Configure > started");
            _hostBuilder.Configure(hostBuilder, configureDelegate);
            _logger.LogDebug("HostBuilder > Configure > ended");
        }
    }
}
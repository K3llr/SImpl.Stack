using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseHostBuilder : IDotNetStackHostBuilder
    {
        private readonly IDotNetStackHostBuilder _hostBuilder;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseHostBuilder(IDotNetStackHostBuilder hostBuilder, ILogger<VerboseHost> logger)
        {
            _hostBuilder = hostBuilder;
            _logger = logger;
           
            _logger.LogInformation("Verbosity is ON");
        }

        public IHost Build()
        {
            _logger.LogDebug("HostBuilder > Building host started");
            var host = new VerboseHost(_hostBuilder.Build(), _logger);
            _logger.LogDebug("HostBuilder > Building host ended");
            
            return host;
        }

        public IHostBuilder ConfigureAppConfiguration(
            Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _hostBuilder.ConfigureAppConfiguration(configureDelegate);
            return this;
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(
            Action<HostBuilderContext, TContainerBuilder> configureDelegate)
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
            _logger.LogDebug("HostBuilder > ConfigureServices started");
            _hostBuilder.ConfigureServices(configureDelegate);
            _logger.LogDebug("HostBuilder > ConfigureServices ended");
            
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
            IServiceProviderFactory<TContainerBuilder> factory)
        {
            _hostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(
            Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            _hostBuilder.UseServiceProviderFactory(factory);
            return this;
        }

        public IDictionary<object, object> Properties => _hostBuilder.Properties;
        public IDotNetStackRuntime Runtime => _hostBuilder.Runtime;
        public void Use<TModule>(Func<TModule> factory) 
            where TModule : IDotNetStackModule
        {
            _hostBuilder.Use(factory);
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory) 
            where TModule : IDotNetStackModule
        {
            return _hostBuilder.AttachNewOrGetConfiguredModule(factory);
        }

        public void Configure(Action<IDotNetStackHostBuilder> configureDelegate)
        {
            _logger.LogDebug("HostBuilder > Module configuration started");
            _hostBuilder.Configure(configureDelegate);
            _logger.LogDebug("HostBuilder > Module configuration ended");
        }
    }
}
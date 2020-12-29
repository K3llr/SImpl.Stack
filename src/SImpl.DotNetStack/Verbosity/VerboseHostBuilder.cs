using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Core;

namespace SImpl.DotNetStack.Diagnostics
{
    public class DiagnosticsHostBuilder : IHostBuilder
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly IDotNetStackRuntime _runtime;

        public DiagnosticsHostBuilder(IHostBuilder hostBuilder, IDotNetStackRuntime runtime)
        {
            _hostBuilder = hostBuilder;
            _runtime = runtime;
        }
        
        public IHost Build()
        {
            return new DiagnosticsHost(_hostBuilder.Build(), _runtime);
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
    }
}
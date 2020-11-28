using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Hosted.GenericHost.Configuration;
using Novicell.App.Hosted.Modules;

namespace Novicell.App.Hosted.GenericHost.HostBuilders
{
    public class ConsoleHostBuilder : IHostBuilder, IConsoleHostBuilder
    {
        private readonly IHostBuilder _hostBuilder;
        private readonly IConsoleConfiguration _consoleConfiguration;

        public ConsoleHostBuilder(IHostBuilder hostBuilder, IConsoleConfiguration consoleConfiguration)
        {
            _hostBuilder = hostBuilder;
            _consoleConfiguration = consoleConfiguration;
        }

        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            _consoleConfiguration.UseStartup<TStartup>();
        }
        
        public void Configure(Action<IAppBuilder> appBuilder)
        {
            _consoleConfiguration.UseStartup(appBuilder);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            ConfigureServices((context, collection) =>
            {
                services?.Invoke(collection);
            });
        }
        
        public IHost Build()
        {
            // Boot the stack
            var app = ConsoleBootManager.Boot(_consoleConfiguration.GetConfiguredStartup());
            var startupModules = app.StartupModules;
            
            // run all host builder configuring modules
            foreach (var module in startupModules)
            {
                if (module is IServicesCollectionConfigureModule serviceConfigurableModule)
                {
                    ConfigureServices(services => serviceConfigurableModule.ConfigureServices(services));
                }
            }
            
            // run all host builder configuring modules
            foreach (var module in startupModules)
            {
                if (module is IHostConfigureModule hostConfigurableModule)
                {
                    hostConfigurableModule.ConfigureHost(_hostBuilder);
                }
            }
            
            // Build inner host
            var host = _hostBuilder.Build();
            
            // run all host object configuring modules
            foreach (var module in startupModules)
            {
                if (module is IHostObjectConfigureModule hostObjectConfigureModule)
                {
                    hostObjectConfigureModule.ConfigureHost(host);
                }
            }

            return new ConsoleStackHost(host, app);
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            return _hostBuilder.ConfigureAppConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            return _hostBuilder.ConfigureContainer(configureDelegate);
        }

        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            return _hostBuilder.ConfigureHostConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return _hostBuilder.ConfigureServices(configureDelegate);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            return _hostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            return _hostBuilder.UseServiceProviderFactory(factory);
        }

        public IDictionary<object, object> Properties => _hostBuilder.Properties;
    }
}
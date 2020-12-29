using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.DotNetStack.Core;
using Novicell.DotNetStack.Exceptions;
using Novicell.DotNetStack.Extensions;
using Novicell.DotNetStack.Host;
using Novicell.DotNetStack.Modules;

namespace Novicell.DotNetStack.HostBuilders
{
    public class DotNetStackHostBuilder : IDotNetStackHostBuilder, IHostBuilder
    {
        private readonly Action<IDotNetStackHostBuilder> _configureDelegate;

        public DotNetStackHostBuilder(IDotNetStackRuntime runtime, Action<IDotNetStackHostBuilder> configureDelegate)
        {
            Runtime = runtime;
            _configureDelegate = configureDelegate;
        }

        public IDotNetStackRuntime Runtime { get; }

        public void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var module = Runtime.ModuleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                Runtime.ModuleManager.AttachModule(factory.Invoke());
            }
            else
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var module = Runtime.ModuleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                module = factory.Invoke();
                Runtime.ModuleManager.AttachModule(module);
            }

            return module;
        }

        public IHost Build()
        {
            // Start configuration of the host builder
            _configureDelegate?.Invoke(this);

            var modules = Runtime.ModuleManager.EnabledModules;
            
            modules.ForEach<IPreInitModule>(module =>
            {
                module.PreInit();
            });
            
            // Run all host builder configuring modules
            modules.ForEach<IServicesCollectionConfigureModule>(module =>
            {
                ConfigureServices((hostBuilderContext, services) => module.ConfigureServices(services));
            });

            // Run all host builder configuring modules
            modules.ForEach<IHostConfigureModule>(module =>
            {
                module.ConfigureHost(Runtime.HostBuilder);
            });
            
            // Build the inner host
            var innerHost = Runtime.HostBuilder.Build();
            
            // Run all host object configuring modules
            modules.ForEach<IHostObjectConfigureModule>(module =>
            {
                module.ConfigureHost(innerHost);
            });
            
            return new DotNetStackHost(Runtime, innerHost);
        }
        
        public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureHostConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureAppConfiguration(configureDelegate);
        }

        public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureServices(configureDelegate);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
        {
            return Runtime.HostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
        {
            return Runtime.HostBuilder.UseServiceProviderFactory(factory);
        }

        public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
        {
            return Runtime.HostBuilder.ConfigureContainer(configureDelegate);
        }

        public IDictionary<object, object> Properties => Runtime.HostBuilder.Properties;
    }
}
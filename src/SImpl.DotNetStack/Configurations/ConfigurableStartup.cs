using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Configurations
{
    public class ConfigurableStartup : IStartup
    {
        private Action<IDotNetStackApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _serviceDelegate;

        public ConfigurableStartup()
        {
            _configureDelegate = builder => { };
        }
        
        public ConfigurableStartup(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;
        }
        
        public ConfigurableStartup WithServiceConfiguration(Action<IServiceCollection> serviceDelegate)
        {
            _serviceDelegate = serviceDelegate;

            return this;
        }
        
        public ConfigurableStartup WithConfiguration(Action<IDotNetStackApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _serviceDelegate?.Invoke(services);
        }

        public void Configure(IDotNetStackApplicationBuilder applicationBuilder)
        {
            _configureDelegate?.Invoke(applicationBuilder);
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.GenericHost.Configuration
{
    public class ConfigurableStartup : IStartup
    {
        private Action<IApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _serviceDelegate;

        public ConfigurableStartup()
        {
            _configureDelegate = builder => { };
        }
        
        public ConfigurableStartup(Action<IApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;
        }
        
        public ConfigurableStartup WithServiceConfiguration(Action<IServiceCollection> serviceDelegate)
        {
            _serviceDelegate = serviceDelegate;

            return this;
        }
        
        public ConfigurableStartup WithConfiguration(Action<IApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _serviceDelegate?.Invoke(services);
        }

        public void Configure(IApplicationBuilder app)
        {
            _configureDelegate?.Invoke(app);
        }
    }
}
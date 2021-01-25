using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.Startup
{
    public class GenericHostConfigurableStartup : IGenericHostStackApplicationStartup
    {
        private Action<IGenericHostApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _serviceDelegate;

        public GenericHostConfigurableStartup()
        {
            _configureDelegate = app => { };
            _serviceDelegate = services => { };
        }
        
        public GenericHostConfigurableStartup(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;
        }
        
        public GenericHostConfigurableStartup WithServiceConfiguration(Action<IServiceCollection> serviceDelegate)
        {
            _serviceDelegate = serviceDelegate;

            return this;
        }
        
        public GenericHostConfigurableStartup WithStackApplicationConfiguration(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _serviceDelegate?.Invoke(services);
        }

        public void ConfigureStackApplication(IGenericHostApplicationBuilder builder)
        {
            _configureDelegate?.Invoke(builder);
        }
    }
}
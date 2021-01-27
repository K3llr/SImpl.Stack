using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;

namespace SImpl.Hosts.GenericHost.Startup
{
    public class GenericHostApplicationStartup : IGenericHostApplicationStartup
    {
        private Action<IGenericHostApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _serviceDelegate;

        public GenericHostApplicationStartup()
        {
            _configureDelegate = app => { };
            _serviceDelegate = services => { };
        }
        
        public GenericHostApplicationStartup WithServiceConfiguration(Action<IServiceCollection> serviceDelegate)
        {
            _serviceDelegate = serviceDelegate;
            return this;
        }
        
        public GenericHostApplicationStartup WithStackApplicationConfiguration(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;
            return this;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            _serviceDelegate?.Invoke(services);
        }

        public void ConfigureApplication(IGenericHostApplicationBuilder builder)
        {
            _configureDelegate?.Invoke(builder);
        }
    }
}
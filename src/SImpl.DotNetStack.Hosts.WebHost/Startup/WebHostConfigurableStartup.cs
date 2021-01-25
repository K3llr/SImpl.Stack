using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;

namespace SImpl.DotNetStack.Hosts.WebHost.Startup
{
    public class WebHostConfigurableStartup : IInternalStackApplicationStartup
    {
        private Action<IWebHostApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _serviceDelegate;

        public WebHostConfigurableStartup()
        {
            _configureDelegate = app => { };
            _serviceDelegate = services => { };
        }
        
        public WebHostConfigurableStartup(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;
        }
        
        public WebHostConfigurableStartup WithServiceConfiguration(Action<IServiceCollection> serviceDelegate)
        {
            _serviceDelegate = serviceDelegate;

            return this;
        }
        
        public WebHostConfigurableStartup WithStackApplicationConfiguration(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _configureDelegate = configureDelegate;

            return this;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _serviceDelegate?.Invoke(services);
        }

        public void ConfigureStackApplication(IWebHostApplicationBuilder app)
        {
            _configureDelegate?.Invoke(app);
        }
    }
}
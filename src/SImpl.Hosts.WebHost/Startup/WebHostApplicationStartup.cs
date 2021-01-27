using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;

namespace SImpl.Hosts.WebHost.Startup
{
    public class WebHostApplicationStartup : IInternalWebHostApplicationStartup
    {
        private Action<IWebHostApplicationBuilder> _configureDelegate;
        private Action<IServiceCollection> _serviceDelegate;

        public WebHostApplicationStartup()
        {
            _configureDelegate = app => { };
            _serviceDelegate = services => { };
        }
        
        public WebHostApplicationStartup WithServiceConfiguration(Action<IServiceCollection> serviceDelegate)
        {
            _serviceDelegate = serviceDelegate;

            return this;
        }
        
        public WebHostApplicationStartup WithStackApplicationConfiguration(Action<IWebHostApplicationBuilder> configureDelegate)
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
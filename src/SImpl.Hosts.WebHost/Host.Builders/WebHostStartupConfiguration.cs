using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.WebHost.Startup;

namespace SImpl.Hosts.WebHost.Host.Builders
{
    public class WebHostStartupConfiguration
    {
        private IWebHostApplicationStartup _startup;
        private readonly WebHostApplicationStartup _startupConfiguration = new WebHostApplicationStartup();
        
        public void UseStartup(IWebHostApplicationStartup startup)
        {
            _startup = startup;
        }
        
        public void ConfigureStackApplication(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _startupConfiguration.WithStackApplicationConfiguration(configureDelegate);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _startupConfiguration.WithServiceConfiguration(services);
        }

        public IInternalWebHostApplicationStartup GetConfiguredStartup()
        {
            if (_startup != null)
            {
                return _startupConfiguration
                    .WithStackApplicationConfiguration(_startup.ConfigureStackApplication);
            }
            
            return _startupConfiguration;
        }
    }
}
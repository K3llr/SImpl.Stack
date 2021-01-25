using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;

namespace SImpl.Stack.Hosts.WebHost.Startup
{
    public class WebHostStartupConfiguration
    {
        private IWebHostStackApplicationStartup _startup;
        private readonly WebHostConfigurableStartup _configurableStartup = new WebHostConfigurableStartup();

        public void UseStartup<TStartup>()
            where TStartup : IWebHostStackApplicationStartup, new()
        {
            UseStartup(new TStartup());
        }
        
        public void UseStartup(IWebHostStackApplicationStartup startup)
        {
            _startup = startup;
        }
        
        public void ConfigureStackApplication(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _configurableStartup.WithStackApplicationConfiguration(configureDelegate);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _configurableStartup.WithServiceConfiguration(services);
        }

        public IInternalStackApplicationStartup GetConfiguredStartup()
        {
            if (_startup != null)
            {
                return _configurableStartup
                    .WithStackApplicationConfiguration(_startup.ConfigureStackApplication);
            }
            
            return _configurableStartup;
        }
    }
}
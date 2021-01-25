using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.Startup
{
    public class GenericHostStartupConfiguration
    {
        private IGenericHostStackApplicationStartup _startup;
        private readonly GenericHostConfigurableStartup _configurableStartup = new GenericHostConfigurableStartup();

        public void UseStartup<TStartup>()
            where TStartup : IGenericHostStackApplicationStartup, new()
        {
            UseStartup(new TStartup());
        }
        
        public void UseStartup(IGenericHostStackApplicationStartup startup)
        {
            _startup = startup;
        }
        
        public void ConfigureStackApplication(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _configurableStartup.WithStackApplicationConfiguration(configureDelegate);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _configurableStartup.WithServiceConfiguration(services);
        }

        public IGenericHostStackApplicationStartup GetConfiguredStartup()
        {
            return _startup ?? _configurableStartup;
        }
    }
}
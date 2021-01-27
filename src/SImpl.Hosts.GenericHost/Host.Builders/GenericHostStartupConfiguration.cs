using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.GenericHost.Startup;

namespace SImpl.Hosts.GenericHost.Host.Builders
{
    public class GenericHostStartupConfiguration
    {
        private IGenericHostApplicationStartup _startup;
        private readonly GenericHostApplicationStartup _configuredStartup = new GenericHostApplicationStartup();

        public void UseStartup(IGenericHostApplicationStartup startup)
        {
            _startup = startup;
        }
        
        public void ConfigureApplication(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _configuredStartup.WithStackApplicationConfiguration(configureDelegate);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _configuredStartup.WithServiceConfiguration(services);
        }

        public IGenericHostApplicationStartup GetConfiguredStartup()
        {
            return _startup ?? _configuredStartup;
        }
    }
}
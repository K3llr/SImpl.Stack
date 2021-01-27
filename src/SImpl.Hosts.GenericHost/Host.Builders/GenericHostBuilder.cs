using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.GenericHost.Startup;

namespace SImpl.Hosts.GenericHost.Host.Builders
{
    public class GenericHostBuilder : IGenericHostBuilder
    {
        private readonly GenericHostStartupConfiguration _config;

        public GenericHostBuilder(GenericHostStartupConfiguration startupConfiguration)
        {
            _config = startupConfiguration;
        }

        public IGenericHostBuilder UseStartup<TStartup>()
            where TStartup : IGenericHostApplicationStartup, new()
        {
            UseStartup(new TStartup());
            return this;
        }

        public IGenericHostBuilder UseStartup(IGenericHostApplicationStartup startup)
        {
            _config.UseStartup(startup);
            return this;
        }

        public IGenericHostBuilder ConfigureApplication(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _config.ConfigureApplication(configureDelegate);
            return this;
        }
        
        public IGenericHostBuilder ConfigureServices(Action<IServiceCollection> services)
        {
            _config.ConfigureServices(services);
            return this;
        }
    }
}
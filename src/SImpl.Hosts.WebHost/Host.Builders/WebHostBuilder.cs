using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.WebHost.Startup;

namespace SImpl.Hosts.WebHost.Host.Builders
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private readonly WebHostStartupConfiguration _config;

        public WebHostBuilder(WebHostStartupConfiguration startupConfiguration)
        {
            _config = startupConfiguration;
        }

        public IWebHostBuilder UseStartup<TStartup>()
            where TStartup : IWebHostApplicationStartup, new()
        {
            UseStartup(new TStartup());
            return this;
        }

        public IWebHostBuilder UseStartup(IWebHostApplicationStartup startup)
        {
            _config.UseStartup(startup);
            return this;
        }

        public IWebHostBuilder ConfigureApplication(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _config.ConfigureStackApplication(configureDelegate);
            return this;
        }
        
        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> services)
        {
            _config.ConfigureServices(services);
            return this;
        }
    }
}
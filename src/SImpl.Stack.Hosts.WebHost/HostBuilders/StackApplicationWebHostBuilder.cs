using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Hosts.WebHost.Startup;

namespace SImpl.Stack.Hosts.WebHost.HostBuilders
{
    public class StackApplicationWebHostBuilder : IStackApplicationWebHostBuilder
    {
        private readonly WebHostStartupConfiguration _config;

        public StackApplicationWebHostBuilder(WebHostStartupConfiguration startupConfiguration)
        {
            _config = startupConfiguration;
        }

        public void UseStartup<TStartup>()
            where TStartup : IWebHostStackApplicationStartup, new()
        {
            _config.UseStartup<TStartup>();
        }

        public void UseStartup(IWebHostStackApplicationStartup startup)
        {
            _config.UseStartup(startup);
        }

        public void ConfigureStackApplication(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _config.ConfigureStackApplication(configureDelegate);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _config.ConfigureServices(services);
        }
    }
}
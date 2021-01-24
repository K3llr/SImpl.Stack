using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Startup;

namespace SImpl.DotNetStack.Hosts.GenericHost.HostBuilders
{
    public class GenericHostBuilder : IGenericHostBuilder
    {
        private readonly IStartupConfiguration _config;

        public GenericHostBuilder(GenericHostStartupConfiguration startupConfiguration)
        {
            _config = startupConfiguration;
        }

        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            _config.UseStartup<TStartup>();
        }
        
        public void Configure(Action<IApplicationBuilder> appBuilder)
        {
            _config.UseStartup(appBuilder);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _config.UseServiceConfiguration(services);
        }
    }
}
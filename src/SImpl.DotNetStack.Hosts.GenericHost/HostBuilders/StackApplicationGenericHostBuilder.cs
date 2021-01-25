using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Startup;

namespace SImpl.DotNetStack.Hosts.GenericHost.HostBuilders
{
    public class StackApplicationGenericHostBuilder : IStackApplicationGenericHostBuilder
    {
        private readonly GenericHostStartupConfiguration _config;

        public StackApplicationGenericHostBuilder(GenericHostStartupConfiguration startupConfiguration)
        {
            _config = startupConfiguration;
        }

        public void UseStartup<TStartup>()
            where TStartup : IGenericHostStackApplicationStartup, new()
        {
            _config.UseStartup<TStartup>();
        }
        
        public void ConfigureStackApplication(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            _config.ConfigureStackApplication(configureDelegate);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _config.ConfigureServices(services);
        }
    }
}
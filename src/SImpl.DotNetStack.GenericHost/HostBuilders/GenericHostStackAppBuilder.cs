using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;
using SImpl.DotNetStack.GenericHost.Application;
using SImpl.DotNetStack.GenericHost.Configuration;

namespace SImpl.DotNetStack.GenericHost.HostBuilders
{
    public class GenericHostStackAppBuilder : IGenericHostStackAppBuilder
    {
        private readonly GenericHostStackAppConfiguration _genericHostStackAppConfiguration;

        public GenericHostStackAppBuilder(GenericHostStackAppConfiguration genericHostStackAppConfiguration)
        {
            _genericHostStackAppConfiguration = genericHostStackAppConfiguration;
        }

        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            _genericHostStackAppConfiguration.StartupConfiguration.UseStartup<TStartup>();
        }
        
        public void Configure(Action<IDotNetStackApplicationBuilder> appBuilder)
        {
            _genericHostStackAppConfiguration.StartupConfiguration.UseStartup(appBuilder);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _genericHostStackAppConfiguration.ServicesCollectionConfiguration.Configure(services);
        }

        public IDotNetStackApplication Build()
        {
            return new GenericHostStackApplication(_genericHostStackAppConfiguration);
        }
    }
}
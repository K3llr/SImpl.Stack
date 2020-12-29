using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.GenericHost.Application;
using SImpl.DotNetStack.GenericHost.Configuration;

namespace SImpl.DotNetStack.GenericHost.HostBuilders
{
    public class GenericHostStackAppBuilder : IGenericHostStackAppBuilder
    {
        private readonly IDotNetStackRuntime _runtime;
        private readonly IStartupConfiguration _config;

        public GenericHostStackAppBuilder(IDotNetStackRuntime runtime)
        {
            _runtime = runtime;
            _config = new StartupConfiguration();
        }

        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            _config.UseStartup<TStartup>();
        }
        
        public void Configure(Action<IDotNetStackApplicationBuilder> appBuilder)
        {
            _config.UseStartup(appBuilder);
        }
        
        public void ConfigureServices(Action<IServiceCollection> services)
        {
            _config.UseServiceConfiguration(services);
        }

        public IDotNetStackApplication Build()
        {
            return new GenericHostStackApplication(_runtime, _config.GetConfiguredStartup());
        }
    }
}
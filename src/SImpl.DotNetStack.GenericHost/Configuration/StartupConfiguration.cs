using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;

namespace SImpl.DotNetStack.GenericHost.Configuration
{
    public class StartupConfiguration : IStartupConfiguration
    {
        private IStartup _startups;
        private Action<IServiceCollection> _serviceDelegate;

        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            UseStartup(new TStartup());
        }
        
        public void UseStartup(Action<IDotNetStackApplicationBuilder> appBuilder)
        {
            UseStartup(new ConfigurableStartup(appBuilder));
        }
        
        public void UseStartup(IStartup startup)
        {
            _startups = startup;
        }

        public void UseServiceConfiguration(Action<IServiceCollection> services)
        {
            _serviceDelegate = services;
        }

        public IStartup GetConfiguredStartup()
        {
            return new ConfigurableStartup()
                .WithConfiguration(configureDelegate => _startups.Configure(configureDelegate))
                .WithServiceConfiguration(_serviceDelegate);
        }
    }
}
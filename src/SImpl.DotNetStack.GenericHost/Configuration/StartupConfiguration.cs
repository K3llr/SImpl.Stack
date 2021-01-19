using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.GenericHost.Configuration
{
    public class StartupConfiguration : IStartupConfiguration
    {
        private IStartup _startup;
        private Action<IServiceCollection> _serviceDelegate;

        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            UseStartup(new TStartup());
        }
        
        public void UseStartup(Action<IApplicationBuilder> appBuilder)
        {
            UseStartup(new ConfigurableStartup(appBuilder));
        }
        
        public void UseStartup(IStartup startup)
        {
            _startup = startup;
        }

        public void UseServiceConfiguration(Action<IServiceCollection> services)
        {
            _serviceDelegate = services;
        }

        public IStartup GetConfiguredStartup()
        {
            return new ConfigurableStartup()
                .WithConfiguration(configureDelegate => _startup?.Configure(configureDelegate))
                .WithServiceConfiguration(services =>
                {
                    _serviceDelegate?.Invoke(services);
                    _startup?.ConfigureServices(services);
                });
        }
    }
}
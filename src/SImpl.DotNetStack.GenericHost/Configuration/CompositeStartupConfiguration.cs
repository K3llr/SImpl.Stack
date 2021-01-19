using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.GenericHost.Configuration
{
    public class CompositeStartupConfiguration : IStartupConfiguration
    {
        private readonly IList<IStartup> _startups = new List<IStartup>();
        
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
            _startups.Add(startup);
        }

        public void UseServiceConfiguration(Action<IServiceCollection> services)
        {
            UseStartup(new ConfigurableStartup().WithServiceConfiguration(services));
        }

        public IStartup GetConfiguredStartup()
        {
            return new ConfigurableStartup(appBuilder =>
            {
                foreach (var startup in _startups)
                {
                    startup.Configure(appBuilder);
                }
            }).WithServiceConfiguration(services =>
            {
                foreach (var startup in _startups)
                {
                    startup.ConfigureServices(services);
                }
            });
        }
    }
}
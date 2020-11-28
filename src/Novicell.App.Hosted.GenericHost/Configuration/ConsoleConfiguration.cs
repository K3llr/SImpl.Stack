using System;
using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Console.Configuration;

namespace Novicell.App.Hosted.GenericHost.Configuration
{
    public class ConsoleConfiguration : IConsoleConfiguration
    {
        private IStartup _startups = new ConfigurableStartup(builder => { });
        
        public void UseStartup<TStartup>()
            where TStartup : IStartup, new()
        {
            _startups = new ConfigurableStartup(appBuilder =>
            {
                new TStartup().Configuration(appBuilder);
            });
        }
        
        public void UseStartup(Action<IAppBuilder> appBuilder)
        {
            _startups = new ConfigurableStartup(appBuilder);
        }
        
        public void UseStartup(IStartup startup)
        {
            _startups = startup;
        }
        
        public IStartup GetConfiguredStartup()
        {
            return _startups;
        }
    }
}
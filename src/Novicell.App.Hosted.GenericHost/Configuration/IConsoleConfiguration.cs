using System;
using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;

namespace Novicell.App.Hosted.GenericHost.Configuration
{
    public interface IConsoleConfiguration
    {
        void UseStartup<TStartup>()
            where TStartup : IStartup, new();

        void UseStartup(Action<IAppBuilder> appBuilder);
        
        void UseStartup(IStartup startup);
        
        IStartup GetConfiguredStartup();
    }
}
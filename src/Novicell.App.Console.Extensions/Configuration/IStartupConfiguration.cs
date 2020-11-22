using System;
using Novicell.App.Console.AppBuilders;

namespace Novicell.App.Console.Extensions.Configuration
{
    public interface IStartupConfiguration
    {
        void UseStartup<TStartup>()
            where TStartup : IStartup, new();

        void UseStartup(Action<IAppBuilder> appBuilder);
        void UseStartup(IStartup startup);
        IStartup GetConfiguredStartup();
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;

namespace Novicell.App.Hosted.GenericHost.HostBuilders
{
    public interface IConsoleHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IStartup, new();

        void Configure(Action<IAppBuilder> appBuilder);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
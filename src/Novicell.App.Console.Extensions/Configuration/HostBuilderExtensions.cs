using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App.Console.Extensions.HostBuilders;

namespace Novicell.App.Console.Extensions.Configuration
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureConsoleStackHost(this IHostBuilder hostBuilder, Action<IConsoleHostBuilder> consoleHostBuilder)
        {
            var hostConfig = new StartupConfiguration();
            
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsoleAppHostedService>();
                services.AddSingleton<IStartupConfiguration>(s => hostConfig);
            });
            
            consoleHostBuilder?.Invoke(new ConsoleHostBuilder(hostBuilder, hostConfig));
            
            return hostBuilder;
        }
    }

    
}
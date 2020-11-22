using System;
using Microsoft.Extensions.DependencyInjection;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Console.Extensions.HostBuilders;

namespace Novicell.App.Console.Extensions.Configuration
{
    public static class ConsoleHostBuilderExtensions
    {
        public static void UseStartup<TStartup>(this IConsoleHostBuilder consoleHostBuilder)
            where TStartup : IStartup, new()
        {
            consoleHostBuilder.CompositeStartupConfiguration.UseStartup<TStartup>();
        }
        
        public static void Configure(this IConsoleHostBuilder consoleHostBuilder, Action<IAppBuilder> appBuilder)
        {
            consoleHostBuilder.CompositeStartupConfiguration.UseStartup(appBuilder);
        }
        
        public static void ConfigureServices(this IConsoleHostBuilder consoleHostBuilder, Action<IServiceCollection> services)
        {
            consoleHostBuilder.HostBuilder.ConfigureServices((context, collection) =>
            {
                services?.Invoke(collection);
            });
        }
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App.Hosted.GenericHost.Configuration;
using Novicell.App.Hosted.GenericHost.HostBuilders;

namespace Novicell.App.Hosted.GenericHost.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureGenericStackHost(this IHostBuilder hostBuilder, Action<IConsoleHostBuilder> consoleHostBuilder)
        {
            var hostConfig = new ConsoleConfiguration();
            
            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                //services.AddHostedService<ConsoleAppHostedService>();
                services.AddSingleton<IConsoleConfiguration>(s => hostConfig);
            });

            var builder = new ConsoleHostBuilder(hostBuilder, hostConfig);
            
            consoleHostBuilder?.Invoke(builder);
            
            return builder;
        }
    }
}
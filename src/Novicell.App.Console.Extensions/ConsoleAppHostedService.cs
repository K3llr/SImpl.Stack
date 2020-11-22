using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Novicell.App.Console.Extensions.Configuration;

namespace Novicell.App.Console.Extensions
{
    public class ConsoleAppHostedService : IHostedService
    {
        private readonly IStartupConfiguration _startupConfiguration;

        public ConsoleAppHostedService(IHostApplicationLifetime appLifetime, IStartupConfiguration startupConfiguration)
        {
            _startupConfiguration = startupConfiguration;
            
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var app = ConsoleBootManager.Boot(_startupConfiguration.GetConfiguredStartup());
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            ConsoleBootManager.UnloadApp();
            
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}
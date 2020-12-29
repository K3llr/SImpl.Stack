using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Novicell.App.Console;
using Novicell.App.Hosted.GenericHost.Configuration;
using Novicell.DotNetStack.Application;

namespace Novicell.App.Hosted.GenericHost.Application
{
    public class GenericHostStackApplication : IDotNetStackApplication
    {
        private readonly GenericHostStackAppConfiguration _configuration;

        public GenericHostStackApplication(GenericHostStackAppConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureService(IServiceCollection services)
        {
            _configuration.ServicesCollectionConfiguration.ConfigureDelegate?.Invoke(services);
        }

        public Task StartAsync()
        {
            // TODO: Change to new startup and app
            var startup = _configuration.StartupConfiguration.GetConfiguredStartup();
            //ConsoleBootManager.Boot(startup);
            
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            ConsoleBootManager.UnloadApp();
                
            return Task.CompletedTask;
        }
    }
}
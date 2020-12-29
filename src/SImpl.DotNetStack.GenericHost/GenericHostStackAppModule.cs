using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Novicell.App.Hosted.GenericHost.HostBuilders;
using Novicell.DotNetStack.Application;
using Novicell.DotNetStack.Modules;

namespace Novicell.App.Hosted.GenericHost
{
    public class GenericHostStackAppModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule
    {
        public GenericHostStackAppBuilder ApplicationBuilder { get; }

        private IDotNetStackApplication _application;

        public GenericHostStackAppModule(GenericHostStackAppBuilder applicationBuilder)
        {
            ApplicationBuilder = applicationBuilder;
        }
        
        public string Name { get; } = "Generic Host Stack App Module";
        
        public void PreInit()
        {
             _application = ApplicationBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _application.ConfigureService(services);
        }

        public async Task StartAsync()
        {
            await _application.StartAsync();
        }

        public async Task StopAsync()
        {
            await _application.StopAsync();
        }
    }
}
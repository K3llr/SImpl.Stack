using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.GenericHost.HostBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.GenericHost
{
    public class GenericHostStackAppModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule, IDiagnosticsModule
    {
        public GenericHostStackAppBuilder ApplicationBuilder { get; }

        private IDotNetStackApplication _application;

        public GenericHostStackAppModule(GenericHostStackAppBuilder applicationBuilder)
        {
            ApplicationBuilder = applicationBuilder;
        }
        
        public string Name { get; } = "Generic Host Stack App Module";
        
        public void Diagnose(IDiagnosticsCollector collector)
        {
            // TODO:
            var section = new PropertiesDiagnosticsSection
            {
                Headline = Name
            };
            
            collector.AddSection("GenericHostStackAppModule", section);
        }

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
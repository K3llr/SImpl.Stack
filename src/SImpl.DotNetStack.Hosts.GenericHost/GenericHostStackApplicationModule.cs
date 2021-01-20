using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.GenericHost
{
    public class GenericHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule, IDiagnosticsModule
    {
        private readonly IGenericStackApplicationBuilder _applicationBuilder;
        private IDotNetStackApplication _application;

        public GenericHostStackApplicationModule(IGenericStackApplicationBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
        }

        public string Name { get; } = "Generic Host Stack Application Module";

        public void Diagnose(IDiagnosticsCollector collector)
        {
            // TODO: Add diagnostics
            collector.AddSection("GenericHostStackAppModule", new PropertiesDiagnosticsSection
            {
                Headline = Name,
                Properties =
                {
                    {"Application Modules", "TODO"}
                }
            });
        }

        public void PreInit()
        {
            _applicationBuilder.Configure();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            _applicationBuilder.ConfigureServices(services);
        }

        public async Task StartAsync()
        {
            _application = _applicationBuilder.Build();

            await _application.StartAsync();
        }

        public async Task StopAsync()
        {
            await _application.StopAsync();
        }
    }
}
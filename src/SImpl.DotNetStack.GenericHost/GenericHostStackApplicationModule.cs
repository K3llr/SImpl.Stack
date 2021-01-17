using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.GenericHost
{
    public class GenericHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule, IDiagnosticsModule
    {
        private readonly IStartup _startup;
        private readonly IDotNetStackApplicationBuilder _applicationBuilder;

        private IDotNetStackApplication _application;

        public GenericHostStackApplicationModule(IStartup startup, IDotNetStackApplicationBuilder applicationBuilder)
        {
            _startup = startup;
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
                    { "Application Modules", "TODO"}
                }
            });
        }

        public void PreInit()
        {
            _applicationBuilder.Configure(_startup.Configure);
            _applicationBuilder.ConfigureApplication();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _startup?.ConfigureServices(services);
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
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.GenericHost.Configuration;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.GenericHost
{
    public class GenericHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule,
        IStartableModule, IDiagnosticsModule
    {
        private readonly IStartup _startup;
        private readonly IApplicationBuilder _applicationBuilder;

        private IDotNetStackApplication _application;

        public GenericHostStackApplicationModule(IStartup startup, IApplicationBuilder applicationBuilder)
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
                    {"Application Modules", "TODO"}
                }
            });
        }

        public void PreInit()
        {
            _startup.Configure(_applicationBuilder);
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
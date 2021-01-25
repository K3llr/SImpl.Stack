using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.GenericHost
{
    public class GenericHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule, IDiagnosticsModule
    {
        private readonly IGenericHostApplicationBuilder _applicationBuilder;
        private readonly Action<IGenericHostApplicationBuilder> _configureDelegate;
        private readonly Action<IServiceCollection> _servicesDelegate;
        
        private IDotNetStackApplication _application;

        public GenericHostStackApplicationModule(IGenericHostApplicationBuilder applicationBuilder, Action<IGenericHostApplicationBuilder> configureDelegate, Action<IServiceCollection> servicesDelegate)
        {
            _applicationBuilder = applicationBuilder;
            _configureDelegate = configureDelegate;
            _servicesDelegate = servicesDelegate;
        }

        public string Name { get; } = nameof(GenericHostStackApplicationModule);

        public void Diagnose(IDiagnosticsCollector collector)
        {
            // TODO: Add diagnostics
            collector.AddSection("GenericHostStackAppModule", new PropertiesDiagnosticsSection
            {
                Headline = Name,
                Properties = { }
            });
        }

        public void PreInit()
        {
            _applicationBuilder.Configure(_configureDelegate);
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            _servicesDelegate?.Invoke(services);
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
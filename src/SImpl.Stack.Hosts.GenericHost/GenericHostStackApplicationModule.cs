using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.Application;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Modules;

namespace SImpl.Stack.Hosts.GenericHost
{
    public class GenericHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule
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
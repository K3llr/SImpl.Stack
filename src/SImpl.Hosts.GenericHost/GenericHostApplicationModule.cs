using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application;
using SImpl.Application.Builders;
using SImpl.Modules;

namespace SImpl.Hosts.GenericHost
{
    public class GenericHostApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule
    {
        private readonly IGenericHostApplicationBuilder _applicationBuilder;
        private readonly Action<IGenericHostApplicationBuilder> _configureDelegate;
        private readonly Action<IServiceCollection> _servicesDelegate;
        
        private ISImplApplication _application;

        public GenericHostApplicationModule(IGenericHostApplicationBuilder applicationBuilder, Action<IGenericHostApplicationBuilder> configureDelegate, Action<IServiceCollection> servicesDelegate)
        {
            _applicationBuilder = applicationBuilder;
            _configureDelegate = configureDelegate;
            _servicesDelegate = servicesDelegate;
        }

        public string Name { get; } = nameof(GenericHostApplicationModule);

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
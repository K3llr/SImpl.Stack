using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application;
using SImpl.Application.Builders;
using SImpl.Hosts.WebHost.AspNetCore;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost
{
    public class WebHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule
    {
        private readonly IWebHostApplicationBuilder _applicationBuilder;
        private readonly Action<IWebHostApplicationBuilder> _configureDelegate;
        private readonly Action<IServiceCollection> _servicesDelegate;

        private ISImplApplication _application;

        public WebHostStackApplicationModule(IWebHostApplicationBuilder applicationBuilder, Action<IWebHostApplicationBuilder> configureDelegate, Action<IServiceCollection> servicesDelegate)
        {
            _applicationBuilder = applicationBuilder;
            _configureDelegate = configureDelegate;
            _servicesDelegate = servicesDelegate;
        }
        
        public string Name { get; } = nameof(WebHostStackApplicationModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, WebHostApplicationStartupFilter>();
            _servicesDelegate?.Invoke(services);
        }

        public void PreInit()
        {
            _applicationBuilder.Configure(_configureDelegate);
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
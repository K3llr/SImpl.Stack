using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Hosts.WebHost.AspNetCore;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Hosts.WebHost
{
    public class WebHostStackApplicationModule : IPreInitModule, IServicesCollectionConfigureModule, IStartableModule, IDiagnosticsModule
    {
        private readonly IWebHostApplicationBuilder _applicationBuilder;
        private readonly Action<IWebHostApplicationBuilder> _configureDelegate;
        
        private IDotNetStackApplication _application;

        public WebHostStackApplicationModule(IWebHostApplicationBuilder applicationBuilder, Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _applicationBuilder = applicationBuilder;
            _configureDelegate = configureDelegate;
        }
        
        public string Name { get; } = nameof(WebHostStackApplicationModule);
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, WebHostStackApplicationStartupFilter>();
        }

        public void PreInit()
        {
            _applicationBuilder.Configure(_configureDelegate);
        }

        public void Diagnose(IDiagnosticsCollector collector)
        {
            // TODO: Add diagnostics
            collector.AddSection("WebHostStackApplicationModule", new PropertiesDiagnosticsSection
            {
                Headline = Name,
                Properties = { }
            });
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
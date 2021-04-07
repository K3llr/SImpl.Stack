using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.WebHost.Startup;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Hosts.WebHost.Host.Builders
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly WebHostStartupConfiguration _config;

        public WebHostBuilder(IModuleManager moduleManager, WebHostStartupConfiguration startupConfiguration)
        {
            _moduleManager = moduleManager;
            _config = startupConfiguration;
        }

        public IWebHostBuilder UseStartup<TStartup>()
            where TStartup : IWebHostApplicationStartup, new()
        {
            UseStartup(new TStartup());
            return this;
        }

        public IWebHostBuilder UseStartup(IWebHostApplicationStartup startup)
        {
            _config.UseStartup(startup);
            return this;
        }

        public IWebHostBuilder ConfigureApplication(Action<IWebHostApplicationBuilder> configureDelegate)
        {
            _config.ConfigureStackApplication(configureDelegate);
            return this;
        }
        
        public IWebHostBuilder ConfigureServices(Action<IServiceCollection> services)
        {
            _config.ConfigureServices(services);
            return this;
        }

        public IWebHostBuilder UseWebHostModule<TModule>(Func<TModule> factory)
            where TModule : IWebHostModule
        {
            _moduleManager.AttachModule(factory.Invoke());
            return this;
        }

        public TModule AttachNewWebHostModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IWebHostModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }
    }
}
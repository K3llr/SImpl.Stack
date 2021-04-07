using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.WebHost.Startup;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost.Host.Builders
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseStartup<TStartup>()
            where TStartup : IWebHostApplicationStartup, new();

        IWebHostBuilder UseStartup(IWebHostApplicationStartup startup);
        
        IWebHostBuilder ConfigureApplication(Action<IWebHostApplicationBuilder> app);
        
        IWebHostBuilder ConfigureServices(Action<IServiceCollection> services);
        
        IWebHostBuilder UseWebHostModule<TModule>(Func<TModule> factory)
            where TModule : IWebHostModule;

        TModule AttachNewWebHostModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IWebHostModule;
    }
}
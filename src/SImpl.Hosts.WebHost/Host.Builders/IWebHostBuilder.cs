using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.WebHost.Startup;

namespace SImpl.Hosts.WebHost.Host.Builders
{
    public interface IWebHostBuilder
    {
        IWebHostBuilder UseStartup<TStartup>()
            where TStartup : IWebHostApplicationStartup, new();

        IWebHostBuilder UseStartup(IWebHostApplicationStartup startup);
        
        IWebHostBuilder ConfigureApplication(Action<IWebHostApplicationBuilder> app);
        
        IWebHostBuilder ConfigureServices(Action<IServiceCollection> services);
    }
}
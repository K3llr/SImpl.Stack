using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Application.Builders;
using SImpl.Hosts.GenericHost.Startup;

namespace SImpl.Hosts.GenericHost.Host.Builders
{
    public interface IGenericHostBuilder
    {
        IGenericHostBuilder UseStartup<TStartup>()
            where TStartup : IGenericHostApplicationStartup, new();

        IGenericHostBuilder UseStartup(IGenericHostApplicationStartup startup);

        IGenericHostBuilder ConfigureApplication(Action<IGenericHostApplicationBuilder> app);
        
        IGenericHostBuilder ConfigureServices(Action<IServiceCollection> services);
    }
}
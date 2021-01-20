using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Configuration;

namespace SImpl.DotNetStack.Hosts.GenericHost.HostBuilders
{
    public interface IGenericHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IStartup, new();

        void Configure(Action<IApplicationBuilder> appBuilder);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
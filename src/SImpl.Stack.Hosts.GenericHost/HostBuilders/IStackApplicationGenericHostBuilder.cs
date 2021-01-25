using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Hosts.GenericHost.Startup;

namespace SImpl.Stack.Hosts.GenericHost.HostBuilders
{
    public interface IStackApplicationGenericHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IGenericHostStackApplicationStartup, new();

        void ConfigureStackApplication(Action<IGenericHostApplicationBuilder> app);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
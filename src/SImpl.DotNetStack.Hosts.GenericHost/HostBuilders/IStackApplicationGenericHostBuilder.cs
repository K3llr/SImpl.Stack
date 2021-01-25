using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Startup;

namespace SImpl.DotNetStack.Hosts.GenericHost.HostBuilders
{
    public interface IStackApplicationGenericHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IGenericHostStackApplicationStartup, new();

        void ConfigureStackApplication(Action<IGenericHostApplicationBuilder> app);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
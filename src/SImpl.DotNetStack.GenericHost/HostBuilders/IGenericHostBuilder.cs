using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.GenericHost.Configuration;

namespace SImpl.DotNetStack.GenericHost.HostBuilders
{
    public interface IGenericHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IStartup, new();

        void Configure(Action<IApplicationBuilder> appBuilder);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
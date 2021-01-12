using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Configurations;

namespace SImpl.DotNetStack.GenericHost.HostBuilders
{
    public interface IGenericHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IStartup, new();

        void Configure(Action<IDotNetStackApplicationBuilder> appBuilder);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
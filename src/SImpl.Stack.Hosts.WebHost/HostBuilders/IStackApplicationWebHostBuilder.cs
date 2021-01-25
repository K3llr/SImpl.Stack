using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Hosts.WebHost.Startup;

namespace SImpl.Stack.Hosts.WebHost.HostBuilders
{
    public interface IStackApplicationWebHostBuilder
    {
        void UseStartup<TStartup>()
            where TStartup : IWebHostStackApplicationStartup, new();

        void UseStartup(IWebHostStackApplicationStartup startup);
        
        void ConfigureStackApplication(Action<IWebHostApplicationBuilder> app);
        
        void ConfigureServices(Action<IServiceCollection> services);
    }
}
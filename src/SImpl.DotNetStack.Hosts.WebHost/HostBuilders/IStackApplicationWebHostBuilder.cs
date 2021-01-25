using System;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Hosts.WebHost.Startup;

namespace SImpl.DotNetStack.Hosts.WebHost.HostBuilders
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
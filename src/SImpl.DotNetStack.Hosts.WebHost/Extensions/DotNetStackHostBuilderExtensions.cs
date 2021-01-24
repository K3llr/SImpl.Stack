using System;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Hosts.WebHost.Startup;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.WebHost.Extensions
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void ConfigureWebHostStackApp(this IDotNetStackHostBuilder hostBuilder, Action<IWebHostApplicationBuilder> configureDelegate)
        {
            var stackApplicationBuilder = DotNetStackRuntimeServices.Current.BootContainer.New<WebHostApplicationBuilder>();
            
            hostBuilder.AttachNewOrGetConfiguredModule(() => new WebHostStackApplicationModule(stackApplicationBuilder, configureDelegate));
        }

        public static void ConfigureWebHostStackApp<TStartup>(this IDotNetStackHostBuilder hostBuilder)
            where TStartup : IWebHostStackApplicationStartup, new()
        {
            ConfigureWebHostStackApp(hostBuilder, new TStartup().ConfigureStackApplication);
        }
    }
}
using System;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Hosts.WebHost.HostBuilders;
using SImpl.DotNetStack.Hosts.WebHost.Startup;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.WebHost.Extensions
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void ConfigureWebHostStackApp(this IDotNetStackHostBuilder hostBuilder, Action<IStackApplicationWebHostBuilder> configureDelegate)
        {
            // Configure Generic host
            var startupConfiguration = new WebHostStartupConfiguration();
            var applicationHostBuilder = new StackApplicationWebHostBuilder(startupConfiguration);
            configureDelegate?.Invoke(applicationHostBuilder);
            
            // Get startup and application builder 
            var startup = startupConfiguration.GetConfiguredStartup();
            var stackApplicationBuilder = DotNetStackRuntimeServices.Current.BootContainer.New<WebHostApplicationBuilder>();
            
            // attach application
            hostBuilder.AttachNewOrGetConfiguredModule(() => new WebHostStackApplicationModule(stackApplicationBuilder, startup.ConfigureStackApplication, startup.ConfigureServices));
        }
    }
}
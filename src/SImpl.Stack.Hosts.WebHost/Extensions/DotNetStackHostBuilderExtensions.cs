using System;
using SImpl.Stack.HostBuilders;
using SImpl.Stack.Hosts.WebHost.ApplicationBuilder;
using SImpl.Stack.Hosts.WebHost.HostBuilders;
using SImpl.Stack.Hosts.WebHost.Startup;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Hosts.WebHost.Extensions
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
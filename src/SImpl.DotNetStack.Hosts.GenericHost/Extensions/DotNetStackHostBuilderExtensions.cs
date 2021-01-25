using System;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.HostBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Startup;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.GenericHost.Extensions
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void ConfigureGenericHostStackApp(this IDotNetStackHostBuilder hostBuilder, Action<IStackApplicationGenericHostBuilder> configureDelegate)
        {
            // Configure Generic host
            var genericHostConfig = new GenericHostStartupConfiguration();
            var genericHostBuilder = new StackApplicationGenericHostBuilder(genericHostConfig);
            configureDelegate?.Invoke(genericHostBuilder);
            
            // Get startup and application builder 
            var startup = genericHostConfig.GetConfiguredStartup();
            var stackApplicationBuilder = DotNetStackRuntimeServices.Current.BootContainer.New<GenericHostApplicationBuilder>();
            
            // attach application
            hostBuilder.AttachNewOrGetConfiguredModule(
                () => new GenericHostStackApplicationModule(stackApplicationBuilder, startup.ConfigureStackApplication, startup.ConfigureServices));
        }
    }
}
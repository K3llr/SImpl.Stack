using System;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.HostBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Startup;

namespace SImpl.DotNetStack.Hosts.GenericHost.Extensions
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void ConfigureGenericHost(this IDotNetStackHostBuilder stackHostBuilder, Action<IGenericHostBuilder> configureDelegate)
        {
            // Configure Generic host
            var genericHostConfig = new GenericHostStartupConfiguration();
            var genericHostBuilder = new GenericHostBuilder(genericHostConfig);
            configureDelegate?.Invoke(genericHostBuilder);
            
            // Get startup and configure application builder 
            var startup = genericHostConfig.GetConfiguredStartup();
            var applicationBuilder = new ApplicationBuilder(startup.Configure, startup.ConfigureServices); 
            applicationBuilder.Configure();
        }
    }
}
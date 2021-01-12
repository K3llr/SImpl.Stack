using System;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.GenericHost.Configuration;
using SImpl.DotNetStack.GenericHost.HostBuilders;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.GenericHost.Extensions
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void ConfigureGenericHost(this IDotNetStackHostBuilder stackHostBuilder, Action<IGenericHostBuilder> configureDelegate)
        {
            // Configure Generic host
            var config = new StartupConfiguration();
            var genericHostBuilder = new GenericHostBuilder(config);
            configureDelegate?.Invoke(genericHostBuilder);
            
            // Get startup and application builder 
            var startup = config.GetConfiguredStartup();
            var applicationBuilder = stackHostBuilder.Runtime.Container.Resolve<IDotNetStackApplicationBuilder>();
            
            // attach application to host stack
            stackHostBuilder.AttachNewOrGetConfiguredModule(() => new GenericHostStackApplicationModule(startup, applicationBuilder));
        }
    }
}
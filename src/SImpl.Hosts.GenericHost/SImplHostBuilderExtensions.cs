using System;
using SImpl.Host.Builders;
using SImpl.Hosts.GenericHost.Application.Builders;
using SImpl.Hosts.GenericHost.Host.Builders;
using SImpl.Hosts.GenericHost.Startup;
using SImpl.Runtime.Core;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Hosts.GenericHost
{
    public static class SImplHostBuilderExtensions
    {
        public static void ConfigureGenericHostStackApp(this ISImplHostBuilder hostBuilder, Action<IGenericHostBuilder> configureDelegate)
        {
            // Configure Generic host
            var genericHostConfig = new GenericHostStartupConfiguration();
            var genericHostBuilder = new GenericHostBuilder(genericHostConfig);
            configureDelegate?.Invoke(genericHostBuilder);
            
            // Get startup and application builder 
            var startup = genericHostConfig.GetConfiguredStartup();
            var stackApplicationBuilder = RuntimeServices.Current.BootContainer.New<GenericHostApplicationBuilder>();
            
            // attach application
            hostBuilder.AttachNewOrGetConfiguredModule(
                () => new GenericHostApplicationModule(stackApplicationBuilder, startup.ConfigureApplication, startup.ConfigureServices));
        }
    }
}
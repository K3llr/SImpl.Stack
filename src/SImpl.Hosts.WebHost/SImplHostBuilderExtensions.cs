using System;
using SImpl.Hosts.WebHost.Application.Builders;
using SImpl.Hosts.WebHost.Host.Builders;
using SImpl.Hosts.WebHost.Startup;
using SImpl.Runtime.Core;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Hosts.WebHost
{
    public static class SImplHostBuilderExtensions
    {
        public static void ConfigureWebHostStackApp(this ISImplHostBuilder hostBuilder, Action<IWebHostBuilder> configureDelegate)
        {
            // Configure Generic host
            var startupConfiguration = new WebHostStartupConfiguration();
            var applicationHostBuilder = new WebHostBuilder(startupConfiguration);
            configureDelegate?.Invoke(applicationHostBuilder);
            
            // Get startup and application builder 
            var startup = startupConfiguration.GetConfiguredStartup();
            var stackApplicationBuilder = RuntimeServices.Current.BootContainer.New<WebHostApplicationBuilder>();
            
            // attach application
            hostBuilder.AttachNewOrGetConfiguredModule(() => new WebHostStackApplicationModule(stackApplicationBuilder, startup.ConfigureStackApplication, startup.ConfigureServices));
        }
    }
}
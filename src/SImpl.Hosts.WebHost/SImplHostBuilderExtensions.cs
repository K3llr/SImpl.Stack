using System;
using System.Collections.Generic;
using SImpl.Host.Builders;
using SImpl.Hosts.WebHost.Application.Builders;
using SImpl.Hosts.WebHost.Host.Builders;
using SImpl.Runtime.Core;

namespace SImpl.Hosts.WebHost
{
    public static class SImplHostBuilderExtensions
    {
        public static ISImplHostBuilder ConfigureWebHostStackApp(this ISImplHostBuilder hostBuilder, Action<IWebHostBuilder> configureDelegate = null)
        {
            // Configure Generic host
            var startupConfiguration = new WebHostStartupConfiguration();
            var applicationHostBuilder = RuntimeServices.Current.BootContainer.New<WebHostBuilder>(new Dictionary<Type, object>
            {
                [typeof(WebHostStartupConfiguration)] = startupConfiguration
            });
            configureDelegate?.Invoke(applicationHostBuilder);
            
            // Get startup and application builder 
            var startup = startupConfiguration.GetConfiguredStartup();
            var stackApplicationBuilder = RuntimeServices.Current.BootContainer.New<WebHostApplicationBuilder>();
            
            // attach application
            hostBuilder.AttachNewOrGetConfiguredModule(() => new WebHostStackApplicationModule(stackApplicationBuilder, startup.ConfigureStackApplication, startup.ConfigureServices));

            return hostBuilder;
        }
    }
}
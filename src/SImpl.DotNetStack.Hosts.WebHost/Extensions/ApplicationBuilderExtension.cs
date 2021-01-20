using System;
using Microsoft.AspNetCore.Builder;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;

namespace SImpl.DotNetStack.Hosts.WebHost.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseDotNetStackWebApp(this IApplicationBuilder webHostApplicationBuilder, Action<IWebApplicationBuilder> configureDelegate)
        {
            // var applicationBuilder = DotNetStackRuntime.Current.Container.Resolve<IDotNetStackWebApplicationBuilder>();
            // attach application to host stack
            //DotNetStackRuntime.Current.HostBuilder.AttachNewOrGetConfiguredModule(() => new GenericHostStackApplicationModule(startup, applicationBuilder));
     
            // Add
        }
    }
}
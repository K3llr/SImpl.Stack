using System;
using Microsoft.AspNetCore.Builder;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.WebHost.ApplicationBuilder;

namespace SImpl.DotNetStack.WebHost.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseDotNetStackWebApp(this IApplicationBuilder webHostApplicationBuilder, Action<IDotNetStackWebApplicationBuilder> configureDelegate)
        {
            // var applicationBuilder = DotNetStackRuntime.Current.Container.Resolve<IDotNetStackWebApplicationBuilder>();
            // attach application to host stack
            //DotNetStackRuntime.Current.HostBuilder.AttachNewOrGetConfiguredModule(() => new GenericHostStackApplicationModule(startup, applicationBuilder));
     
        }
    }
}
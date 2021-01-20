using System;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.GenericHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDotNetStackGenericApp(this IApplicationBuilder applicationBuilder, Action<IGenericStackApplicationBuilder> configureDelegate)
        {
            var stackApplicationBuilder = new GenericStackApplicationBuilder(DotNetStackRuntimeServices.Current.ApplicationBuilder, configureDelegate, applicationBuilder.ConfigureServices);
            
            // attach application
            applicationBuilder.HostBuilder.AttachNewOrGetConfiguredModule(() => new GenericHostStackApplicationModule(stackApplicationBuilder));
        }
    }
}
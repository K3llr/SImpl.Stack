using System;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.GenericHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDotNetStackGenericApp(this IApplicationBuilder applicationBuilder, Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            if (DotNetStackRuntimeServices.Current.Flags.Verbose)
            {
                // TODO: attach verbose decorators
            }
            
            var stackApplicationBuilder = DotNetStackRuntimeServices.Current.BootContainer.New<GenericHostApplicationBuilder>();
            
            // attach application
            DotNetStackRuntimeServices.Current.HostBuilder.AttachNewOrGetConfiguredModule(
                () => new GenericHostStackApplicationModule(stackApplicationBuilder, configureDelegate, applicationBuilder.ConfigureServices));
        }
    }
}
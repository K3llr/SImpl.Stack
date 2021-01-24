using System;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Hosts.WebHost.Extensions
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void ConfigureStackWebHostApp(this IDotNetStackHostBuilder hostBuilder, Action<IWebHostApplicationBuilder> configureDelegate)
        {
            if (DotNetStackRuntimeServices.Current.Flags.Verbose)
            {
                // TODO: attach verbose decorators
            }

            var stackApplicationBuilder = DotNetStackRuntimeServices.Current.BootContainer.New<WebHostApplicationBuilder>();
            
            hostBuilder.AttachNewOrGetConfiguredModule(() => new WebHostStackApplicationModule(stackApplicationBuilder, configureDelegate));
        }
    }
}
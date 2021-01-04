using System;
using SImpl.DotNetStack.GenericHost.HostBuilders;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.GenericHost.Extensions
{
    public static class DotNetStackBuilderExtensions
    {
        public static void UseGenericHostStackApp(this IDotNetStackHostBuilder stackHostBuilder, Action<IGenericHostStackAppBuilder> configureDelegate)
        {
            var module = stackHostBuilder.AttachNewOrGetConfiguredModule(() =>
            {
                var builder = new GenericHostStackAppBuilder(stackHostBuilder.Runtime);
                return new GenericHostStackAppModule(builder);
            });
            
            configureDelegate?.Invoke(module.ApplicationBuilder);
        }
    }
}
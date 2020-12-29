using System;
using Novicell.App.Hosted.GenericHost.Configuration;
using Novicell.App.Hosted.GenericHost.HostBuilders;
using Novicell.DotNetStack.HostBuilders;

namespace Novicell.App.Hosted.GenericHost.Extensions
{
    public static class DotNetStackBuilderExtensions
    {
        public static void UseGenericHostStackApp(this IDotNetStackHostBuilder stackHostBuilder, Action<IGenericHostStackAppBuilder> stackAppBuilder)
        {
            System.Console.WriteLine($"AttachModule: {nameof(GenericHostStackAppModule)}");
            
            var module = stackHostBuilder.AttachNewOrGetConfiguredModule(() =>
            {
                var config = new GenericHostStackAppConfiguration();
                var builder = new GenericHostStackAppBuilder(config);
                
                return new GenericHostStackAppModule(builder);
            });
            
            stackAppBuilder?.Invoke(module.ApplicationBuilder);
        }
    }
}
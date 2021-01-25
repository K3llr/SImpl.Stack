using System;
using Novicell.App.AppBuilders;
using Novicell.App.Console;
using SImpl.Stack.ApplicationBuilders;

namespace SImpl.DotNetStack.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseNovicellConsoleApp(this IDotNetStackApplicationBuilder stackBuilder, Action<IConsoleAppBuilder> configure)
        {
            // TODO: Need to be application modules only
            stackBuilder.AttachNewStackAppModuleOrGetConfigured(() => new NovicellAppConsoleRuntimeModule(configure));
            
            // Execute Startup configuration (custom configuration)
            configure?.Invoke(ConsoleBootManager.CurrentConsoleBuilder);
        }
    }
}
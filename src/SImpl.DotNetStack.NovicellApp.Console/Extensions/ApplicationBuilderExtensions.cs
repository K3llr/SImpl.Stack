using System;
using Novicell.App.AppBuilders;
using Novicell.App.Console;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseNovicellConsoleApp(this IDotNetStackApplicationBuilder stackBuilder, Action<IConsoleAppBuilder> configure)
        {
            // Execute Startup configuration (custom configuration)
            configure(ConsoleBootManager.CurrentConsoleBuilder);
        }
    }
}
using System;
using Novicell.App.AppBuilders;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Console.Configuration;

namespace Novicell.App.Console.Extensions.Configuration
{
    public static class AppBuilderExtensions
    {
        public static void UseConsoleStackApp(this IAppBuilder appBuilder, Action<IConsoleAppBuilder> configure)
        {
            appBuilder.UseNovicellConsoleApp(configure);
        }
    }
}
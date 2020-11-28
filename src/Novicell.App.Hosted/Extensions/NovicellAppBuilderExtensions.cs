using System;
using Novicell.App.AppBuilders;

namespace Novicell.App.Hosted.Extensions
{
    
    public static class NovicellAppBuilderExtensions
    {
        public static void Use<TConsoleModule>(this INovicellAppBuilder appBuilder)
            where TConsoleModule : IModule, new()
        {
            appBuilder.Use(() => new TConsoleModule());
        }
        
        public static void Use<TConsoleModule>(this INovicellAppBuilder appBuilder, Func<TConsoleModule> factory)
            where TConsoleModule : IModule
        {
            var existingModule = appBuilder.GetModule<TConsoleModule>();
            if (existingModule is null)
            {
                appBuilder.AttachModule(factory());
            }
        }
    }
}
using System;
using Novicell.App;
using Novicell.App.AppBuilders;

namespace SImpl.DotNetStack.App.Extensions
{
    
    public static class NovicellAppBuilderExtensions
    {
        public static void Use<TModule>(this INovicellAppBuilder appBuilder)
            where TModule : IModule, new()
        {
            appBuilder.Use(() => new TModule());
        }
        
        public static void Use<TModule>(this INovicellAppBuilder appBuilder, Func<TModule> factory)
            where TModule : IModule
        {
            var existingModule = appBuilder.GetModule<TModule>();
            if (existingModule is null)
            {
                appBuilder.AttachModule(factory());
            }
        }
        
        public static void Use(this INovicellAppBuilder appBuilder, IModule module)
        {
            appBuilder.Use(() => module);
        }
        
        public static TModule AttachOrGetExistingConfiguredModule<TModule>(this INovicellAppBuilder appBuilder, Func<TModule> factory)
            where TModule : IModule
        {
            var module = appBuilder.GetModule<TModule>();
            if (module is null)
            {
                module = factory.Invoke();
                appBuilder.Use(module);
            }

            return module;
        }
    }
}
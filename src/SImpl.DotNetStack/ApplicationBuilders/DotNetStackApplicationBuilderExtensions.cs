using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public static class DotNetStackApplicationBuilderExtensions
    {
        public static void UseStackAppModule<TModule>(this IDotNetStackApplicationBuilder stackAppBuilder)
            where TModule : IApplicationModule, new()
        {
            stackAppBuilder.UseStackAppModule(() => new TModule());
        }
        
        public static void UseStackAppModule<TModule>(this IDotNetStackApplicationBuilder stackAppBuilder, TModule module)
            where TModule : IApplicationModule
        {
            stackAppBuilder.UseStackAppModule(() => module);
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this IDotNetStackApplicationBuilder stackAppBuilder)
            where TModule : IApplicationModule, new()
        {
            return stackAppBuilder.AttachNewStackAppModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this IDotNetStackApplicationBuilder stackAppBuilder, TModule module)
            where TModule : IApplicationModule
        {
            return stackAppBuilder.AttachNewStackAppModuleOrGetConfigured(() => module);
        }
    }
}
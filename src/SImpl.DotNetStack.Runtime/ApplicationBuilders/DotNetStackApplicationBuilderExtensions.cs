using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Runtime.ApplicationBuilders
{
    public static class DotNetStackApplicationBuilderExtensions
    {
        public static void Use<TModule>(this IDotNetStackApplicationBuilder stackHostBuilder)
            where TModule : IApplicationModule, new()
        {
            stackHostBuilder.Use(() => new TModule());
        }
        
        public static void Use<TModule>(this IDotNetStackApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IApplicationModule
        {
            stackHostBuilder.Use(() => module);
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this IDotNetStackApplicationBuilder stackHostBuilder)
            where TModule : IApplicationModule, new()
        {
            return stackHostBuilder.AttachNewOrGetConfiguredModule(() => new TModule());
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this IDotNetStackApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IApplicationModule
        {
            return stackHostBuilder.AttachNewOrGetConfiguredModule(() => module);
        }
    }
}
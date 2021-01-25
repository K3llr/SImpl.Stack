using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public static class GenericHostApplicationBuilderExtensions
    {
        public static void UseGenericHostStackAppModule<TModule>(this IGenericHostApplicationBuilder builder) 
            where TModule : IGenericHostApplicationModule, new()
        {
            builder.UseGenericHostStackAppModule<TModule>(() => new TModule());
        }
        
        public static void UseGenericHostStackAppModule<TModule>(this IGenericHostApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IGenericHostApplicationModule
        {
            stackHostBuilder.UseGenericHostStackAppModule(() => module);
        }
        
        public static TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(this IGenericHostApplicationBuilder stackHostBuilder)
            where TModule : IGenericHostApplicationModule, new()
        {
            return stackHostBuilder.AttachNewGenericHostStackAppModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(this IGenericHostApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IGenericHostApplicationModule
        {
            return stackHostBuilder.AttachNewGenericHostStackAppModuleOrGetConfigured(() => module);
        }
    }
}
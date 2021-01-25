using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public static class WebHostApplicationBuilderExtensions
    {
        public static void UseWebHostStackAppModule<TModule>(this IWebHostApplicationBuilder builder) 
            where TModule : IWebHostApplicationModule, new()
        {
            builder.UseWebHostStackAppModule<TModule>(() => new TModule());
        }
        
        public static void UseWebHostStackAppModule<TModule>(this IWebHostApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IWebHostApplicationModule
        {
            stackHostBuilder.UseWebHostStackAppModule(() => module);
        }
        
        public static TModule AttachNewWebHostStackAppModuleOrGetConfigured<TModule>(this IWebHostApplicationBuilder stackHostBuilder)
            where TModule : IWebHostApplicationModule, new()
        {
            return stackHostBuilder.AttachNewWebHostStackAppModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewWebHostStackAppModuleOrGetConfigured<TModule>(this IWebHostApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IWebHostApplicationModule
        {
            return stackHostBuilder.AttachNewWebHostStackAppModuleOrGetConfigured(() => module);
        }
    }
}
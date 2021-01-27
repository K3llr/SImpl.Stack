using SImpl.Application.Builders;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost
{
    public static class WebHostApplicationBuilderExtensions
    {
        public static IWebHostApplicationBuilder UseWebHostAppModule<TModule>(this IWebHostApplicationBuilder builder) 
            where TModule : IWebHostApplicationModule, new()
        {
            return builder.UseWebHostAppModule(() => new TModule());
        }
        
        public static IWebHostApplicationBuilder UseWebHostAppModule<TModule>(this IWebHostApplicationBuilder builder, TModule module)
            where TModule : IWebHostApplicationModule
        {
            return builder.UseWebHostAppModule(() => module);
        }
        
        public static TModule AttachNewWebHostAppModuleOrGetConfigured<TModule>(this IWebHostApplicationBuilder stackHostBuilder)
            where TModule : IWebHostApplicationModule, new()
        {
            return stackHostBuilder.AttachNewWebHostAppModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewWebHostAppModuleOrGetConfigured<TModule>(this IWebHostApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IWebHostApplicationModule
        {
            return stackHostBuilder.AttachNewWebHostAppModuleOrGetConfigured(() => module);
        }
    }
}
using SImpl.Hosts.WebHost.Host.Builders;
using SImpl.Modules;

namespace SImpl.Hosts.WebHost
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseWebHostAppModule<TModule>(this IWebHostBuilder builder) 
            where TModule : IWebHostModule, new()
        {
            return builder.UseWebHostModule(() => new TModule());
        }
        
        public static IWebHostBuilder UseWebHostAppModule<TModule>(this IWebHostBuilder builder, TModule module)
            where TModule : IWebHostModule
        {
            return builder.UseWebHostModule(() => module);
        }
        
        public static TModule AttachNewWebHostAppModuleOrGetConfigured<TModule>(this IWebHostBuilder stackHostBuilder)
            where TModule : IWebHostModule, new()
        {
            return stackHostBuilder.AttachNewWebHostModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewWebHostAppModuleOrGetConfigured<TModule>(this IWebHostBuilder stackHostBuilder, TModule module)
            where TModule : IWebHostModule
        {
            return stackHostBuilder.AttachNewWebHostModuleOrGetConfigured(() => module);
        }
    }
}
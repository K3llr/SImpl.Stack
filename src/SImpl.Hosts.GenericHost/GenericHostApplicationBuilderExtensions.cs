using SImpl.Application.Builders;
using SImpl.Modules;

namespace SImpl.Hosts.GenericHost
{
    public static class GenericHostApplicationBuilderExtensions
    {
        public static IGenericHostApplicationBuilder UseGenericHostAppModule<TModule>(this IGenericHostApplicationBuilder builder) 
            where TModule : IGenericHostApplicationModule, new()
        {
            return builder.UseGenericHostAppModule<TModule>(() => new TModule());
        }
        
        public static IGenericHostApplicationBuilder UseGenericHostAppModule<TModule>(this IGenericHostApplicationBuilder builder, TModule module)
            where TModule : IGenericHostApplicationModule
        {
            return builder.UseGenericHostAppModule(() => module);
        }
        
        public static TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(this IGenericHostApplicationBuilder stackHostBuilder)
            where TModule : IGenericHostApplicationModule, new()
        {
            return stackHostBuilder.AttachNewGenericHostAppModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(this IGenericHostApplicationBuilder stackHostBuilder, TModule module)
            where TModule : IGenericHostApplicationModule
        {
            return stackHostBuilder.AttachNewGenericHostAppModuleOrGetConfigured(() => module);
        }
    }
}
using SImpl.Application.Builders;
using SImpl.Modules;

namespace SImpl.Runtime
{
    public static class SImplApplicationBuilderExtensions
    {
        public static ISImplApplicationBuilder UseAppModule<TModule>(this ISImplApplicationBuilder stackAppBuilder)
            where TModule : IApplicationModule, new()
        {
            return stackAppBuilder.UseAppModule(() => new TModule());
        }
        
        public static ISImplApplicationBuilder UseAppModule<TModule>(this ISImplApplicationBuilder stackAppBuilder, TModule module)
            where TModule : IApplicationModule
        {
            return stackAppBuilder.UseAppModule(() => module);
        }
        
        public static TModule AttachNewAppModuleOrGetConfigured<TModule>(this ISImplApplicationBuilder stackAppBuilder)
            where TModule : IApplicationModule, new()
        {
            return stackAppBuilder.AttachNewAppModuleOrGetConfigured(() => new TModule());
        }
        
        public static TModule AttachNewAppModuleOrGetConfigured<TModule>(this ISImplApplicationBuilder stackAppBuilder, TModule module)
            where TModule : IApplicationModule
        {
            return stackAppBuilder.AttachNewAppModuleOrGetConfigured(() => module);
        }
    }
}
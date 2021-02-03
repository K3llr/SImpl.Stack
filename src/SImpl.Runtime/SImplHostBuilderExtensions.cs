using SImpl.Host.Builders;
using SImpl.Modules;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime
{
    public static class SImplHostBuilderExtensions
    {
        public static void Use<TModule>(this ISImplHostBuilder stackHostBuilder)
            where TModule : ISImplModule, new()
        {
            stackHostBuilder.Use(() => new TModule());
        }
        
        public static void Use<TModule>(this ISImplHostBuilder stackHostBuilder, TModule module)
            where TModule : ISImplModule
        {
            stackHostBuilder.Use(() => module);
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this ISImplHostBuilder stackHostBuilder)
            where TModule : ISImplModule, new()
        {
            return stackHostBuilder.AttachNewOrGetConfiguredModule(() => new TModule());
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this ISImplHostBuilder stackHostBuilder, TModule module)
            where TModule : ISImplModule
        {
            return stackHostBuilder.AttachNewOrGetConfiguredModule(() => module);
        }
    }
}
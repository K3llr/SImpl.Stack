using Novicell.DotNetStack.Modules;

namespace Novicell.DotNetStack.HostBuilders
{
    public static class DotNetStackHostBuilderExtensions
    {
        public static void Use<TModule>(this IDotNetStackHostBuilder stackHostBuilder)
            where TModule : IDotNetStackModule, new()
        {
            stackHostBuilder.Use(() => new TModule());
        }
        
        public static void Use<TModule>(this IDotNetStackHostBuilder stackHostBuilder, TModule module)
            where TModule : IDotNetStackModule
        {
            stackHostBuilder.Use(() => module);
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this IDotNetStackHostBuilder stackHostBuilder)
            where TModule : IDotNetStackModule, new()
        {
            return stackHostBuilder.AttachNewOrGetConfiguredModule(() => new TModule());
        }
        
        public static TModule AttachNewOrGetConfiguredModule<TModule>(this IDotNetStackHostBuilder stackHostBuilder, TModule module)
            where TModule : IDotNetStackModule
        {
            return stackHostBuilder.AttachNewOrGetConfiguredModule(() => module);
        }
    }
}
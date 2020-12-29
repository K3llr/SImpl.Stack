using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.GenericHost.DependencyInjection.Configuration
{
    public static class DotNetStackAppBuilderExtensions
    {
        public static void UseDependencyInjection(this IDotNetStackHostBuilder stackHostBuilder)
        {
            stackHostBuilder.AttachNewOrGetConfiguredModule(() =>
            {
                var genericHostConfig = new GenericHostDependencyInjectionConfig(null); // TODO: <-- this is an error
                return new GenericHostDependencyInjectionModule(genericHostConfig);
            });
        }
    }
}
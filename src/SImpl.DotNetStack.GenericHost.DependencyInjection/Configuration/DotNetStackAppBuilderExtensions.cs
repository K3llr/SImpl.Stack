using Novicell.DotNetStack.HostBuilders;

namespace Novicell.App.Hosted.GenericHost.DependencyInjection.Configuration
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
using SImpl.Runtime.Host.Builders;

namespace SImpl.DotNetStack.GenericHost.DependencyInjection.Configuration
{
    public static class DotNetStackAppBuilderExtensions
    {
        public static void UseDependencyInjection(this ISImplHostBuilder stackHostBuilder)
        {
            stackHostBuilder.AttachNewOrGetConfiguredModule(() =>
            {
                var genericHostConfig = new GenericHostDependencyInjectionConfig(null); // TODO: <-- this is an error
                return new GenericHostDependencyInjectionModule(genericHostConfig);
            });
        }
    }
}
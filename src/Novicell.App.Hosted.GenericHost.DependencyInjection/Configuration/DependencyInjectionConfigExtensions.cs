using Novicell.App.DependencyInjection.Configuration;
using Novicell.App.Hosted.Extensions;

namespace Novicell.App.Hosted.GenericHost.DependencyInjection.Configuration
{
    public static class DependencyInjectionConfigExtensions
    {
        public static void UseGenericHost(this DependencyInjectionConfig config)
        {
            config.NovicellAppBuilder.Use(() =>
            {
                var genericHostConfig = new GenericHostDependencyInjectionConfig(config);
                return new GenericHostDependencyInjectionModule(genericHostConfig);
            });
        }
    }
}
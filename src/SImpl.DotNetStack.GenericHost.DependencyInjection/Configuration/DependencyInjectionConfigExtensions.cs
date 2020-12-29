using Novicell.App.DependencyInjection.Configuration;
using SImpl.DotNetStack.App.Extensions;

namespace SImpl.DotNetStack.GenericHost.DependencyInjection.Configuration
{
    public static class DependencyInjectionConfigExtensions
    {
        public static void UseGenericHost(this DependencyInjectionConfig config)
        {
            var module = config.NovicellAppBuilder.AttachOrGetExistingConfiguredModule(() =>
            {
                var genericHostConfig = new GenericHostDependencyInjectionConfig(config);
                return new GenericHostDependencyInjectionModule(genericHostConfig);
            });
            
            //var dotNetStackModule = DotNetStackRuntime.Current.ModuleManager.GetModule<GenericHostDependencyInjectionModule>();
            
        }
    }
}
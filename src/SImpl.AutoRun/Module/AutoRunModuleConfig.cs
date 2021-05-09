using SImpl.AutoRun.Services;
using SImpl.Factories;
using SImpl.Reflect.Providers;

namespace SImpl.AutoRun.Module
{
    public class AutoRunModuleConfig
    {
        public AutoRunModuleConfig()
        {
            AutoRunDiscoveryServiceFactory =
                new LambdaFactory<IAutoRunDiscoveryStrategy>(
                    () => new ReflectionAutoRunDiscoveryStrategy(new ForceLoadAssemblyProvider()));
        }
        
        public IFactory<IAutoRunDiscoveryStrategy> AutoRunDiscoveryServiceFactory { get; private set; }

        public void OverrideAutoRunDiscoveryService(IFactory<IAutoRunDiscoveryStrategy> factory)
        {
            AutoRunDiscoveryServiceFactory = factory;
        }
    }
}
using System;
using System.Collections.Generic;

namespace SImpl.AutoRun.Services
{
    public class ConfigurableAutoRunDiscoveryStrategy: IAutoRunDiscoveryStrategy
    {
        private readonly Func<IEnumerable<AutoRunModuleInfo>> _lambdaDiscoveryStrategy;

        public ConfigurableAutoRunDiscoveryStrategy(Func<IEnumerable<AutoRunModuleInfo>> lambdaDiscoveryStrategy)
        {
            _lambdaDiscoveryStrategy = lambdaDiscoveryStrategy;
        }
        
        public IEnumerable<AutoRunModuleInfo> Discover()
        {
            return _lambdaDiscoveryStrategy.Invoke();
        }
    }
}
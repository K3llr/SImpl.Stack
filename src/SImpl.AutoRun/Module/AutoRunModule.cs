using System;
using SImpl.Host.Builders;
using SImpl.Modules;

namespace SImpl.AutoRun.Module
{
    public class AutoRunModule : IPreInitModule
    {
        private readonly ISImplHostBuilder _host;
        public string Name { get; } =  nameof(AutoRunModule);
        
        public AutoRunModuleConfig Config { get; }

        public AutoRunModule(ISImplHostBuilder host, AutoRunModuleConfig config)
        {
            _host = host;
            Config = config;
        }
        
        public void PreInit()
        {
            // Create discovery service
            var discoveryStrategy = Config.AutoRunDiscoveryServiceFactory.New();
            
            // Discover and attach modules
            var autoRunModules = discoveryStrategy.Discover();
            foreach (var autoRunModule in autoRunModules)
            {
                var module = _host.GetConfiguredModule<ISImplModule>(autoRunModule.ModuleType);
                if (module is null)
                {
                    _host.Use(autoRunModule.ModuleFactory.Invoke());
                }
            }
        }
    }
}
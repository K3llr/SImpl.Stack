using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Verbosity
{
    public class VerboseModuleManager : IModuleManager
    {
        private readonly IModuleManager _moduleManager;
        private readonly ILogger<VerboseHost> _logger;

        public VerboseModuleManager(IModuleManager moduleManager, ILogger<VerboseHost> logger)
        {
            _moduleManager = moduleManager;
            _logger = logger;
        }
        
        public void AttachModule<TModule>(TModule module) 
            where TModule : IDotNetStackModule
        {
            _logger.LogDebug($"AttachModule ".PadRight(40, '-'));
            _logger.LogDebug($"{typeof(TModule).Name}");

            _logger.LogDebug($"Implements:");
            
            var modules = typeof(TModule)
                .GetInterfaces()
                .Where(t => typeof(IDotNetStackModule).IsAssignableFrom(t)) // Only IDotNetStack interfaces
                .OrderBy(t => t.Name);
            
            foreach (var type in modules)
            {
                _logger.LogDebug($"- {type.Name}");
            }
            
            _logger.LogDebug($"Dependent on:");
            _logger.LogDebug($"- TODO");
            
            _logger.LogDebug($"".PadRight(40, '-'));
            
            _moduleManager.AttachModule(module);
        }

        public void DisableModule<TModule>() 
            where TModule : IDotNetStackModule
        {
            _logger.LogDebug($"DisableModule: {typeof(TModule).Name}");
            _moduleManager.DisableModule<TModule>();
        }

        public TModule GetConfiguredModule<TModule>() 
            where TModule : IDotNetStackModule
        {
            return _moduleManager.GetConfiguredModule<TModule>();
        }

        public TModule GetConfiguredModule<TModule>(Type t) 
            where TModule : IDotNetStackModule
        {
            return _moduleManager.GetConfiguredModule<TModule>(t);
        }

        public void SetModuleState(ModuleState state)
        {
            _logger.LogDebug($"Set module state: {state:G}");
            _moduleManager.SetModuleState(state);
        }

        public IReadOnlyList<IDotNetStackModule> Modules => _moduleManager.Modules.Select(m => new VerboseModule(m, _logger)).ToList().AsReadOnly();
        public IReadOnlyList<IDotNetStackModule> EnabledModules => _moduleManager.EnabledModules.Select(m => new VerboseModule(m, _logger)).ToList().AsReadOnly();
        public IReadOnlyList<IDotNetStackModule> DisabledModules => _moduleManager.DisabledModules.Select(m => new VerboseModule(m, _logger)).ToList().AsReadOnly();
        public IReadOnlyCollection<ModuleRuntimeInfo> ModuleContexts => _moduleManager.ModuleContexts;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Verbosity
{
    public class VerboseModuleManager : IModuleManager
    {
        private readonly IModuleManager _moduleManager;
        private readonly ILogger _logger;

        public VerboseModuleManager(IModuleManager moduleManager, ILogger logger)
        {
            _moduleManager = moduleManager;
            _logger = logger;
        }
        
        public void AttachModule<TModule>(TModule module) 
            where TModule : ISImplModule
        {
            _logger.LogDebug($"ModuleManager > AttachModule started");
            
            _moduleManager.AttachModule(module);
            LogModule(module);
                
            _logger.LogDebug($"ModuleManager > AttachModule ended");
        }

        public TModule AttachNewOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : ISImplModule
        {
            _logger.LogDebug($"ModuleManager > AttachNewOrGetConfigured started");

            var module = _moduleManager.AttachNewOrGetConfigured(factory);
            LogModule(module);
                
            _logger.LogDebug($"ModuleManager > AttachModule ended");
            
            return module;
        }
        
        public void DisableModule<TModule>() 
            where TModule : ISImplModule
        {
            _logger.LogDebug($"ModuleManager > DisableModule: {typeof(TModule).Name}");
            _moduleManager.DisableModule<TModule>();
        }

        public TModule GetConfiguredModule<TModule>() 
            where TModule : ISImplModule
        {
            return _moduleManager.GetConfiguredModule<TModule>();
        }

        public TModule GetConfiguredModule<TModule>(Type t) 
            where TModule : ISImplModule
        {
            return _moduleManager.GetConfiguredModule<TModule>(t);
        }

        public void SetModuleState(ModuleState state)
        {
            _logger.LogDebug($"ModuleManager > Set module state: {state:G}");
            _moduleManager.SetModuleState(state);
        }
        public IReadOnlyList<ModuleRuntimeInfo> ModuleInfos => _moduleManager.ModuleInfos;

        public IReadOnlyList<ISImplModule> AllModules => _moduleManager.AllModules;
        public IReadOnlyList<ISImplModule> EnabledModules => _moduleManager.EnabledModules;
        public IReadOnlyList<ISImplModule> DisabledModules => _moduleManager.DisabledModules;
        
        private void LogModule<TModule>(TModule module) where TModule : ISImplModule
        {
            var info = new ModuleInfo
            {
                Type = typeof(TModule).Name,
                Name = module.Name,
                Implements = typeof(TModule)
                    .GetInterfaces()
                    .Where(t => typeof(ISImplModule).IsAssignableFrom(t)) // Only ISImplModule interfaces
                    .OrderBy(t => t.Name)
                    .Select(t => t.Name)
                    .ToArray(),
                DependentOn = typeof(TModule)
                    .GetCustomAttribute<DependsOnAttribute>()
                    ?.Dependencies
                    ?.Select(t => t.Name)
                    .ToArray() ?? Array.Empty<string>()
            };

            _logger.LogDebug(JsonSerializer.Serialize(info, new JsonSerializerOptions
            {
                WriteIndented = true,
                
            }));
        }
    }
}
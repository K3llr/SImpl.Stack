using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Modules.Dependencies;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Runtime.Verbosity
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
            _logger.LogDebug($"      ModuleManager > AttachModule started");
            
            _moduleManager.AttachModule(module);
            LogModule(module);
                
            _logger.LogDebug($"      ModuleManager > AttachModule ended");
        }

        private void LogModule<TModule>(TModule module) where TModule : IDotNetStackModule
        {
            _logger.LogDebug("      {");
            _logger.LogDebug($"        \"Type\": \"{typeof(TModule).Name}\",");
            _logger.LogDebug($"        \"Name\": \"{module.Name}\",");

            _logger.LogDebug($"        \"Implements\":");
            _logger.LogDebug("          [");

            var modules = typeof(TModule)
                .GetInterfaces()
                .Where(t => typeof(IDotNetStackModule).IsAssignableFrom(t)) // Only IDotNetStack interfaces
                .OrderBy(t => t.Name);

            foreach (var type in modules)
            {
                _logger.LogDebug($"            \"{type.Name}\",");
            }

            _logger.LogDebug("          ],");


            var dependsOn = module.GetType().GetCustomAttribute<DependsOnAttribute>();
            var dependencies = dependsOn?.Dependencies ?? Array.Empty<Type>();

            if (dependencies.Any())
            {
                _logger.LogDebug($"        \"DependentOn\":");
                _logger.LogDebug("          [");

                foreach (var type in dependencies)
                {
                    _logger.LogDebug($"            \"{type.Name}\",");
                }

                _logger.LogDebug("          ]");
            }
            else
            {
                _logger.LogDebug("        \"DependentOn\": []");
            }

            _logger.LogDebug("      }");
        }

        public TModule AttachNewOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : IDotNetStackModule
        {
            _logger.LogDebug($"      ModuleManager > AttachNewOrGetConfigured started");

            var module = _moduleManager.AttachNewOrGetConfigured(factory);
            LogModule(module);
                
            _logger.LogDebug($"      ModuleManager > AttachModule ended");
            
            return module;
        }
        
        public void DisableModule<TModule>() 
            where TModule : IDotNetStackModule
        {
            _logger.LogDebug($"      ModuleManager > DisableModule: {typeof(TModule).Name}");
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
            _logger.LogDebug($"  ModuleManager > Set module state: {state:G}");
            _moduleManager.SetModuleState(state);
        }
        public IReadOnlyList<ModuleRuntimeInfo> ModuleInfos => _moduleManager.ModuleInfos;

        public IReadOnlyList<IDotNetStackModule> AllModules => _moduleManager.AllModules;
        public IReadOnlyList<IDotNetStackModule> EnabledModules => _moduleManager.EnabledModules;
        public IReadOnlyList<IDotNetStackModule> DisabledModules => _moduleManager.DisabledModules;
    }
}
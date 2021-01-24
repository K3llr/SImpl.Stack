using System;
using System.Collections.Generic;
using System.Linq;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Exceptions;
using SImpl.DotNetStack.Runtime.Extensions;

namespace SImpl.DotNetStack.Runtime.Core
{
    public class ModuleManager : IModuleManager
    {
        private readonly List<ModuleRuntimeInfo> _modulesInfos = new List<ModuleRuntimeInfo>();
        
        public void AttachModule<TModule>(TModule module)
            where TModule : IDotNetStackModule
        {
            var configuredModule = GetConfiguredModule<TModule>();
            if (configuredModule is null)
            {
                var moduleContext = new ModuleRuntimeInfo(module);
                moduleContext.SetState(ModuleState.Attached);

                _modulesInfos.Add(moduleContext);
            }
            else
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }
        }
        
        public TModule AttachNewOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var configuredModule = GetConfiguredModule<TModule>();
            if (configuredModule is not null)
            {
                return configuredModule;
            }
            else
            {
                configuredModule = factory.Invoke();
                AttachModule(configuredModule);
                return configuredModule;
            }
        }

        public void DisableModule<TModule>()
            where TModule : IDotNetStackModule
        {
            var moduleRuntimeInfo = _modulesInfos.SingleOrDefault(ctx => ctx.Module is TModule);
            moduleRuntimeInfo?.Disable();
        }

        public TModule GetConfiguredModule<TModule>()
            where TModule : IDotNetStackModule
        {
            return (TModule)_modulesInfos.SingleOrDefault(ctx => ctx.Module is TModule)?.Module;
        }

        public TModule GetConfiguredModule<TModule>(Type t)
            where TModule : IDotNetStackModule
        {
            return (TModule)_modulesInfos.SingleOrDefault(ctx => ctx.ModuleType == t)?.Module;
        }
        
        public void SetModuleState(ModuleState state)
        {
            foreach (var context in _modulesInfos)
            {
                context.SetState(state);
            }
        }

        public IReadOnlyList<ModuleRuntimeInfo> ModuleInfos => _modulesInfos.AsReadOnly();

        public IReadOnlyList<IDotNetStackModule> AllModules => 
            _modulesInfos.Select(ctx => ctx.Module)
                .ToList()
                .AsReadOnly();

        public IReadOnlyList<IDotNetStackModule> DisabledModules => 
            _modulesInfos
                .Where(ctx => !ctx.IsEnabled)
                .Select(ctx => ctx.Module)
                .ToList()
                .AsReadOnly();
        
        public IReadOnlyList<IDotNetStackModule> EnabledModules => 
            _modulesInfos
                .Where(ctx => ctx.IsEnabled)
                .Select(ctx => ctx.Module)
                .ToList()
                .AsReadOnly();

        public IReadOnlyList<IDotNetStackModule> BootSequence
        {
            get
            {
                var enabledModuleContexts =
                    _modulesInfos
                        .Where(info => info.IsEnabled)
                        .Select(info => new ModuleContext
                        {
                            Info = info
                        }).ToList();

                var moduleContextLookup =
                    enabledModuleContexts
                        .ToDictionary(ctx => ctx.Info.ModuleType);

                foreach (var ctx in enabledModuleContexts)
                {
                    ctx.Dependencies = ctx.Info.Dependencies
                        .Select(t => moduleContextLookup.ContainsKey(t) ? moduleContextLookup[t] : null)
                        .Where(d => d is not null)
                        .ToArray();
                }

                return enabledModuleContexts
                    .TopologicalSort(ctx => ctx.Dependencies)
                    .Select(ctx => ctx.Info.Module)
                    .ToList()
                    .AsReadOnly();
            }
        }
        
        private class ModuleContext
        {
            public ModuleRuntimeInfo Info { get; set; }
            public ModuleContext[] Dependencies { get; set; }
        }
    }
}
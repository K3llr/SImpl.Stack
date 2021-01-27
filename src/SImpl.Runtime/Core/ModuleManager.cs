using System;
using System.Collections.Generic;
using System.Linq;
using SImpl.Modules;
using SImpl.Runtime.Exceptions;

namespace SImpl.Runtime.Core
{
    public class ModuleManager : IModuleManager
    {
        private readonly List<ModuleRuntimeInfo> _modulesInfos = new List<ModuleRuntimeInfo>();
        
        public void AttachModule<TModule>(TModule module)
            where TModule : ISImplModule
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
            where TModule : ISImplModule
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
            where TModule : ISImplModule
        {
            var moduleRuntimeInfo = _modulesInfos.SingleOrDefault(ctx => ctx.Module is TModule);
            moduleRuntimeInfo?.Disable();
        }

        public TModule GetConfiguredModule<TModule>()
            where TModule : ISImplModule
        {
            return (TModule)_modulesInfos.SingleOrDefault(ctx => ctx.Module is TModule)?.Module;
        }

        public TModule GetConfiguredModule<TModule>(Type t)
            where TModule : ISImplModule
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

        public IReadOnlyList<ISImplModule> AllModules => 
            _modulesInfos.Select(ctx => ctx.Module)
                .ToList()
                .AsReadOnly();

        public IReadOnlyList<ISImplModule> DisabledModules => 
            _modulesInfos
                .Where(ctx => !ctx.IsEnabled)
                .Select(ctx => ctx.Module)
                .ToList()
                .AsReadOnly();
        
        public IReadOnlyList<ISImplModule> EnabledModules => 
            _modulesInfos
                .Where(ctx => ctx.IsEnabled)
                .Select(ctx => ctx.Module)
                .ToList()
                .AsReadOnly();
    }
}
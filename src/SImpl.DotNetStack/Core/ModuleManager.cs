using System;
using System.Collections.Generic;
using System.Linq;
using SImpl.DotNetStack.Exceptions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Core
{
    public class ModuleManager : IModuleManager
    {
        private readonly List<ModuleRuntimeInfo> _modules = new List<ModuleRuntimeInfo>();
        private readonly List<Type> _disabledModules = new List<Type>();
        
        public void AttachModule<TModule>(TModule module)
            where TModule : IDotNetStackModule
        {
            var configuredModule = GetConfiguredModule<TModule>();
            if (configuredModule is not null)
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }

            var moduleContext = new ModuleRuntimeInfo(module);
            moduleContext.SetState(ModuleState.Attached);
            
            _modules.Add(moduleContext);
        }

        public void DisableModule<TModule>()
            where TModule : IDotNetStackModule
        {
            _disabledModules.Add(typeof(TModule));
        }

        public TModule GetConfiguredModule<TModule>()
            where TModule : IDotNetStackModule
        {
            return (TModule)_modules.SingleOrDefault(ctx => ctx.Module is TModule)?.Module;
        }

        public TModule GetConfiguredModule<TModule>(Type t)
            where TModule : IDotNetStackModule
        {
            return (TModule)_modules.SingleOrDefault(ctx => ctx.Type == t)?.Module;
        }
        
        public void SetModuleState(ModuleState state)
        {
            foreach (var context in _modules)
            {
                context.SetState(state);
            }
        }

        public IReadOnlyList<IDotNetStackModule> Modules => _modules.Select(ctx => ctx.Module).ToList().AsReadOnly();
        public IReadOnlyList<IDotNetStackModule> DisabledModules => Modules.Where(module => _disabledModules.Contains(module.GetType())).ToList().AsReadOnly();
        public IReadOnlyList<IDotNetStackModule> EnabledModules => Modules.Except(DisabledModules).ToList().AsReadOnly();
        
        public IReadOnlyCollection<ModuleRuntimeInfo> ModuleContexts => _modules.AsReadOnly();

    }
}
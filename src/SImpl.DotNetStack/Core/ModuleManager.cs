using System;
using System.Collections.Generic;
using System.Linq;
using Novicell.DotNetStack.Exceptions;
using Novicell.DotNetStack.Modules;

namespace Novicell.DotNetStack.Core
{
    public class ModuleManager : IModuleManager
    {
        private readonly List<IDotNetStackModule> _modules = new List<IDotNetStackModule>();
        private readonly List<Type> _disabledModules = new List<Type>();
        
        public void AttachModule<TModule>(TModule module)
            where TModule : IDotNetStackModule
        {
            var configuredModule = GetConfiguredModule<TModule>();
            if (configuredModule is not null)
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }
            _modules.Add(module);
        }

        public void DisableModule<TModule>()
            where TModule : IDotNetStackModule
        {
            _disabledModules.Add(typeof(TModule));
        }

        public TModule GetConfiguredModule<TModule>()
            where TModule : IDotNetStackModule
        {
            return (TModule)_modules.SingleOrDefault(module => module is TModule);
        }

        public TModule GetConfiguredModule<TModule>(Type t)
            where TModule : IDotNetStackModule
        {
            return (TModule)_modules.SingleOrDefault(module => module.GetType() == t);
        }

        public IReadOnlyList<IDotNetStackModule> Modules => _modules.AsReadOnly();
        public IReadOnlyList<IDotNetStackModule> DisabledModules => Modules.Where(module => _disabledModules.Contains(module.GetType())).ToList().AsReadOnly();
        public IReadOnlyList<IDotNetStackModule> EnabledModules => Modules.Except(DisabledModules).ToList().AsReadOnly();
    }
}
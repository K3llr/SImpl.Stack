using System;
using System.Reflection;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    /// <summary>
    /// Responsible for maintaining runtime information about attached modules
    /// </summary>
    public class ModuleRuntimeInfo
    {
        public ModuleRuntimeInfo(ISImplModule module)
        {
            Module = module;
            State = ModuleState.New;
            
            var dependsOn = module.GetType().GetCustomAttribute<DependsOnAttribute>();
            Dependencies = dependsOn?.Dependencies ?? Array.Empty<Type>();
        }
        
        public ISImplModule Module { get; }

        public Type[] Dependencies { get; }
        
        public Type ModuleType => Module.GetType();

        public bool IsEnabled { get; private set; } = true;
        
        public void Disable()
        {
            IsEnabled = false;
        }
        
        public ModuleState State { get; private set; }

        public void SetState(ModuleState state)
        {
            State = state;
        }
    }
}
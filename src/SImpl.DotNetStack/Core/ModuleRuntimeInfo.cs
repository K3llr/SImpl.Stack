using System;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Core
{
    /// <summary>
    /// Responsible for maintaining runtime information about attached modules
    /// </summary>
    public class ModuleRuntimeInfo
    {
        public ModuleRuntimeInfo(IDotNetStackModule module)
        {
            Module = module;
            State = ModuleState.New;
        }
        
        public IDotNetStackModule Module { get; }

        public Type Type => Module.GetType();

        public bool Disabled { get; set; } // TODO: Figure out whether this is relevant
        
        public ModuleState State { get; private set; }

        public void SetState(ModuleState state)
        {
            State = state;
        }
        
        // State
        // TODO: Implement verbose diagnostics instead 
    }
}
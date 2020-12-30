using System;
using System.Collections.Generic;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Core
{
    public interface IModuleManager
    {
        void AttachModule<TModule>(TModule module)
            where TModule : IDotNetStackModule;

        void DisableModule<TModule>()
            where TModule : IDotNetStackModule;
        
        TModule GetConfiguredModule<TModule>()
            where TModule : IDotNetStackModule;
        
        TModule GetConfiguredModule<TModule>(Type t)
            where TModule : IDotNetStackModule;
        
        void SetModuleState(ModuleState state);

        IReadOnlyList<IDotNetStackModule> AllModules { get; }
        
        IReadOnlyList<IDotNetStackModule> EnabledModules { get; }
        
        IReadOnlyList<IDotNetStackModule> DisabledModules { get; }
        
        IReadOnlyList<ModuleRuntimeInfo> ModuleInfos { get; }
        
        IReadOnlyList<IDotNetStackModule> BootSequence { get; }
    }
}
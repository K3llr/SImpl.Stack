using System;
using System.Collections.Generic;
using SImpl.Stack.Modules;

namespace SImpl.Stack.Runtime.Core
{
    public interface IModuleManager
    {
        void AttachModule<TModule>(TModule module)
            where TModule : IDotNetStackModule;

        TModule AttachNewOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
        
        void DisableModule<TModule>()
            where TModule : IDotNetStackModule;
        
        TModule GetConfiguredModule<TModule>()
            where TModule : IDotNetStackModule;
        
        TModule GetConfiguredModule<TModule>(Type t)
            where TModule : IDotNetStackModule;
        
        void SetModuleState(ModuleState state);

        IReadOnlyList<ModuleRuntimeInfo> ModuleInfos { get; }
        
        IReadOnlyList<IDotNetStackModule> AllModules { get; }
        
        IReadOnlyList<IDotNetStackModule> EnabledModules { get; }
        
        IReadOnlyList<IDotNetStackModule> DisabledModules { get; }
    }
}
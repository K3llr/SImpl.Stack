using System;
using System.Collections.Generic;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    public interface IModuleManager
    {
        void AttachModule<TModule>(TModule module)
            where TModule : ISImplModule;

        TModule AttachNewOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : ISImplModule;
        
        void DisableModule<TModule>()
            where TModule : ISImplModule;
        
        TModule GetConfiguredModule<TModule>()
            where TModule : ISImplModule;
        
        TModule GetConfiguredModule<TModule>(Type t)
            where TModule : ISImplModule;
        
        void SetModuleState(ModuleState state);

        IReadOnlyList<ModuleRuntimeInfo> ModuleInfos { get; }
        
        IReadOnlyList<ISImplModule> AllModules { get; }
        
        IReadOnlyList<ISImplModule> EnabledModules { get; }
        
        IReadOnlyList<ISImplModule> DisabledModules { get; }
    }
}
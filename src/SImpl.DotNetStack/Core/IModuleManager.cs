using System;
using System.Collections.Generic;
using Novicell.DotNetStack.Modules;

namespace Novicell.DotNetStack.Core
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
        
        IReadOnlyList<IDotNetStackModule> Modules { get; }
        
        IReadOnlyList<IDotNetStackModule> EnabledModules { get; }
        
        IReadOnlyList<IDotNetStackModule> DisabledModules { get; } 
        
        
    }
}
using System;
using Novicell.DotNetStack.Core;
using Novicell.DotNetStack.Modules;

namespace Novicell.DotNetStack.HostBuilders
{
    public interface IDotNetStackHostBuilder
    {
        IDotNetStackRuntime Runtime { get; }

        void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
        
        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
    }
}
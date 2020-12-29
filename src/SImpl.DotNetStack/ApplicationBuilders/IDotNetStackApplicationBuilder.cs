using System;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public interface IDotNetStackApplicationBuilder
    {
        IDotNetStackRuntime Runtime { get; }

        void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
        
        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule;
    }
}
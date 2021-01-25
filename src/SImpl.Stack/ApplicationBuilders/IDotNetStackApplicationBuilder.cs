using System;
using SImpl.Stack.Application;
using SImpl.Stack.Modules;

namespace SImpl.Stack.ApplicationBuilders
{
    public interface IDotNetStackApplicationBuilder
    {
        void UseStackAppModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;

        TModule AttachNewStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;
        
        IDotNetStackApplication Build();
    }
}
using System;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
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
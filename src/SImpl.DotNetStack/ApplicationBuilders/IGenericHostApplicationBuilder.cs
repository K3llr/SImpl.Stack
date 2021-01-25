using System;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public interface IGenericHostApplicationBuilder : IDotNetStackApplicationBuilder
    { 
        void UseGenericHostStackAppModule<TModule>(Func<TModule> factory)
            where TModule : IGenericHostApplicationModule;

        TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IGenericHostApplicationModule;

        void Configure(Action<IGenericHostApplicationBuilder> configureDelegate);
    }
}
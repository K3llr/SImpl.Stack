using System;
using SImpl.Stack.Modules;

namespace SImpl.Stack.ApplicationBuilders
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
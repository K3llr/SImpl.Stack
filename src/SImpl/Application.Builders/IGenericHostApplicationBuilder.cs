using System;
using SImpl.Modules;

namespace SImpl.Application.Builders
{
    public interface IGenericHostApplicationBuilder : ISImplApplicationBuilder
    { 
        IGenericHostApplicationBuilder UseGenericHostAppModule<TModule>(Func<TModule> factory)
            where TModule : IGenericHostApplicationModule;

        TModule AttachNewGenericHostAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IGenericHostApplicationModule;

        void Configure(Action<IGenericHostApplicationBuilder> configureDelegate);
    }
}
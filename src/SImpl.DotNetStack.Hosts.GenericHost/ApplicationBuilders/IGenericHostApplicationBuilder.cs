using System;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Modules;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
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
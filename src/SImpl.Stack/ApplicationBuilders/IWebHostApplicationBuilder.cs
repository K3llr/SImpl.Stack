using System;
using SImpl.Stack.Modules;

namespace SImpl.Stack.ApplicationBuilders
{
    public interface IWebHostApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void UseWebHostStackAppModule<TModule>(Func<TModule> factory)
            where TModule : IWebHostApplicationModule;

        TModule AttachNewWebHostStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IWebHostApplicationModule;

        void Configure(Action<IWebHostApplicationBuilder> configureDelegate);
        
        
    }
}
using System;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
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
using System;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.Modules;

namespace SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder
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
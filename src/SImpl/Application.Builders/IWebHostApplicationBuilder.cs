using System;
using SImpl.Modules;

namespace SImpl.Application.Builders
{
    public interface IWebHostApplicationBuilder : ISImplApplicationBuilder
    {
        IWebHostApplicationBuilder UseWebHostAppModule<TModule>(Func<TModule> factory)
            where TModule : IWebHostApplicationModule;

        TModule AttachNewWebHostAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IWebHostApplicationModule;

        void Configure(Action<IWebHostApplicationBuilder> configureDelegate);
        
        
    }
}
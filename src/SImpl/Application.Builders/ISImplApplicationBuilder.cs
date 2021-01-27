using System;
using SImpl.Modules;

namespace SImpl.Application.Builders
{
    public interface ISImplApplicationBuilder
    {
        ISImplApplicationBuilder UseAppModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;

        TModule AttachNewAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;
        
        ISImplApplication Build();
    }
}
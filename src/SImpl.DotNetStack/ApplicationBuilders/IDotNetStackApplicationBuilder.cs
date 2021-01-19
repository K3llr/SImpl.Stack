using System;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public interface IDotNetStackApplicationBuilder
    {
        void Use<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;
        
        TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule;

        void Configure(Action<IDotNetStackApplicationBuilder> configureDelegate);
        
        IDotNetStackApplication Build();
    }
}
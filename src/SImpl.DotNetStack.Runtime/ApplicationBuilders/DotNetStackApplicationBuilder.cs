using System;
using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Runtime.ApplicationBuilders
{
    public abstract class DotNetStackApplicationBuilder : IDotNetStackApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;

        public DotNetStackApplicationBuilder(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public void UseStackAppModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
        }

        public TModule AttachNewStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public abstract IDotNetStackApplication Build();
    }
}
using System;
using SImpl.Stack.Application;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Runtime.ApplicationBuilders
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
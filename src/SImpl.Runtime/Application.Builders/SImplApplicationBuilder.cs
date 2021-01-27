using System;
using SImpl.Application;
using SImpl.Application.Builders;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Application.Builders
{
    public abstract class SImplApplicationBuilder : ISImplApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;

        public SImplApplicationBuilder(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        public ISImplApplicationBuilder UseAppModule<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
            return this;
        }

        public TModule AttachNewAppModuleOrGetConfigured<TModule>(Func<TModule> factory)
            where TModule : IApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public abstract ISImplApplication Build();
    }
}
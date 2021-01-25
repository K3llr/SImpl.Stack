using System;
using SImpl.Stack.Application;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Hosts.GenericHost.Application;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.ApplicationBuilders;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Hosts.GenericHost.ApplicationBuilders
{
    public class GenericHostApplicationBuilder : DotNetStackApplicationBuilder, IGenericHostApplicationBuilder
    {
        private readonly IModuleManager _moduleManager;
        private readonly IApplicationBootManager _bootManager;
        
        public GenericHostApplicationBuilder(IModuleManager moduleManager, IApplicationBootManager bootManager) 
            : base(moduleManager)
        {
            _moduleManager = moduleManager;
            _bootManager = bootManager;
        }

        public void UseGenericHostStackAppModule<TModule>(Func<TModule> factory) 
            where TModule : IGenericHostApplicationModule
        {
            _moduleManager.AttachModule(factory.Invoke());
        }
        
        public TModule AttachNewGenericHostStackAppModuleOrGetConfigured<TModule>(Func<TModule> factory) 
            where TModule : IGenericHostApplicationModule
        {
            return _moduleManager.AttachNewOrGetConfigured(factory);
        }

        public void Configure(Action<IGenericHostApplicationBuilder> configureDelegate)
        {
            configureDelegate?.Invoke(this);
            
            _bootManager.Configure<IApplicationModule, IDotNetStackApplicationBuilder>(this);
            _bootManager.Configure<IGenericHostApplicationModule, IGenericHostApplicationBuilder>(this);
        }

        public override IDotNetStackApplication Build()
        {
            return new GenericHostApplication(_bootManager);
        }
    }
}
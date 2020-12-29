using System;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Exceptions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public class DotNetStackApplicationBuilder : IDotNetStackApplicationBuilder
    {
        public DotNetStackApplicationBuilder(IDotNetStackRuntime runtime)
        {
            Runtime = runtime;
        }
        
        public IDotNetStackRuntime Runtime { get; }

        public void Use<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var module = Runtime.ModuleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                Runtime.ModuleManager.AttachModule(factory.Invoke());
            }
            else
            {
                throw new InvalidConfigurationException($"A module with the type {nameof(TModule)} has already been attached to the stack.");
            }
        }

        public TModule AttachNewOrGetConfiguredModule<TModule>(Func<TModule> factory)
            where TModule : IDotNetStackModule
        {
            var module = Runtime.ModuleManager.GetConfiguredModule<TModule>();
            if (module is null)
            {
                module = factory.Invoke();
                Runtime.ModuleManager.AttachModule(module);
            }

            return module;
        }
    }
}
using System;
using Novicell.App.AppBuilders;
using SImpl.Stack.HostBuilders;
using SImpl.Stack.Runtime.HostBuilders;

namespace spike.stack.module
{
    public static partial class AppBuilderExtension
    {
        public static void UseDotNetStackTestModule(this IDotNetStackHostBuilder stackHostBuilders)
        {
            stackHostBuilders.Use<DotNetStackTestModule>();
        }
        
        public static void UseHybridTestModule(this IDotNetStackHostBuilder stackHostBuilder)
        {
            stackHostBuilder.Use<HybridTestModule>();
        }
        
        public static void UseHybridTestModule(this INovicellAppBuilder novicellAppBuilder)
        {
            var existingModule = novicellAppBuilder.GetModule<HybridTestModule>();
            if (existingModule is null)
            {
                novicellAppBuilder.AttachModule(new HybridTestModule());
            }
        }
    }
}
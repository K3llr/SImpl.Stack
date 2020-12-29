using System;
using Novicell.App.AppBuilders;
using SImpl.DotNetStack.HostBuilders;

namespace spike.stack.module
{
    public static class AppBuilderExtension
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
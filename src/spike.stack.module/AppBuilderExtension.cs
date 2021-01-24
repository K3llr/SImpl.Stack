using System;
using Novicell.App.AppBuilders;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Runtime.HostBuilders;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;

namespace spike.stack.module
{
    public static partial class AppBuilderExtension
    {
        public static void UseDotNetStackTestModule(this IDotNetStackHostBuilder stackHostBuilders)
        {
            stackHostBuilders.UseStackAppModule<DotNetStackTestModule>();
        }
        
        public static void UseHybridTestModule(this IDotNetStackHostBuilder stackHostBuilder)
        {
            stackHostBuilder.UseStackAppModule<HybridTestModule>();
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
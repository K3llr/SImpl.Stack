using System;
using Novicell.App.AppBuilders;
using SImpl.Host.Builders;
using SImpl.Runtime;
using SImpl.Runtime.Host.Builders;

namespace spike.stack.module
{
    public static partial class AppBuilderExtension
    {
        public static void UseDotNetStackTestModule(this ISImplHostBuilder stackHostBuilders)
        {
            stackHostBuilders.Use<DotNetStackTestModule>();
        }
        
        public static void UseHybridTestModule(this ISImplHostBuilder stackHostBuilder)
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
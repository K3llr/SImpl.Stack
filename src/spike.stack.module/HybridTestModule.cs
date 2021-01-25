using System;
using Novicell.App;
using Novicell.App.AppBuilders;
using SImpl.Stack.Modules;

namespace spike.stack.module
{
    public class HybridTestModule : IDotNetStackModule, INovicellModule
    {
        public void Configure(INovicellAppBuilder appBuilder)
        {
            // Console.WriteLine($"Configure: {Name}");
        }

        public void Init()
        {
            // Console.WriteLine($"Init: {Name}");
        }

        public string Name => nameof(HybridTestModule);
    }
}
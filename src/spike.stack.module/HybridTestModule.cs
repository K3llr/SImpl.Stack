using System;
using Novicell.App;
using Novicell.App.AppBuilders;
using Novicell.DotNetStack.Modules;

namespace spike.stack.module
{
    public class Test02Module : IDotNetStackModule, INovicellModule
    {
        public void Configure(INovicellAppBuilder appBuilder)
        {
            Console.WriteLine($"Configure: {Name}");
        }

        public void Init()
        {
            Console.WriteLine($"Init: {Name}");
        }

        public string Name => "Test 02 module";
    }
}
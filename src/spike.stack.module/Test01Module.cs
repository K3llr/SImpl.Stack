using System;
using Novicell.App;
using Novicell.App.AppBuilders;

namespace spike.stack.module
{
    public class Test01Module : INovicellModule
    {
        public void Configure(INovicellAppBuilder appBuilder)
        {
            Console.WriteLine($"Configure: {Name}");
        }

        public void Init()
        {
            Console.WriteLine($"Init: {Name}");
        }

        public string Name => "Test 01 module";
    }
    
    public class Test02Module : INovicellModule
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
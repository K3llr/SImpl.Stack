using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Modules;

namespace spike.stack.module
{
    public abstract class BaseModule : IStartableModule
    {
        public Task StartAsync(IHost host)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(IHost host)
        {
            return Task.CompletedTask;
        }

        public abstract string Name { get; }
    }
    
    public class RootModule : BaseModule
    {
        public override string Name => nameof(RootModule);
    }
    
    [DependsOn(typeof(RootModule))]
    public class Level1Module : BaseModule
    {
        public override string Name => nameof(Level1Module);
    }
    
    [DependsOn(typeof(Level1Module))]
    public class Level2aModule : BaseModule
    {
        public override string Name => nameof(Level2aModule);
    }
    
    [DependsOn(typeof(Level1Module), typeof(Level2aModule))]
    public class Level2bModule : BaseModule
    {
        public override string Name => nameof(Level2bModule);
    }
}
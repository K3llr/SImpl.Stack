using Microsoft.Extensions.Hosting;

namespace Novicell.DotNetStack.Core
{
    public class DotNetStackRuntime : IDotNetStackRuntime
    {
        private static readonly object Lock = new object();
        public static DotNetStackRuntime Current { get; internal set; }
        
        public static DotNetStackRuntime Init(DotNetStackRuntime runtime)
        {
            if (Current != null) return Current;
            
            lock (Lock)
            {
                return Current ??= runtime;
            }
        }
        
        public static DotNetStackRuntime Init(IHostBuilder hostBuilder)
        {
            if (Current != null) return Current;
            
            lock (Lock)
            {
                return Current ??= new DotNetStackRuntime
                {
                    ModuleManager = new ModuleManager(),
                    HostBuilder = hostBuilder
                };
            }
        }
        
        private DotNetStackRuntime() { }

        public IModuleManager ModuleManager { get; private set; }

        public IHostBuilder HostBuilder { get; private set; }
    }
}
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Diagnostics;

namespace SImpl.DotNetStack.Core
{
    public class DotNetStackRuntime : IDotNetStackRuntime
    {
        private static readonly object Lock = new object();
        public static DotNetStackRuntime Current { get; private set; }
        
        private static DotNetStackRuntime Init(DotNetStackRuntime runtime)
        {
            if (Current != null) return Current;
            
            lock (Lock)
            {
                return Current ??= runtime;
            }
        }

        public DotNetStackRuntime(IHostBuilder hostBuilder, IModuleManager moduleManager, IDiagnosticsCollector diagnostics, RuntimeFlags runtimeFlags)
        {
            HostBuilder = hostBuilder;
            ModuleManager = moduleManager;
            Diagnostics = diagnostics;
            Flags = runtimeFlags;
            
            Init(this);
        }

        public IModuleManager ModuleManager { get; }

        public IHostBuilder HostBuilder { get; }
        
        public IDiagnosticsCollector Diagnostics { get; }
        
        public RuntimeFlags Flags { get; }
    }
}
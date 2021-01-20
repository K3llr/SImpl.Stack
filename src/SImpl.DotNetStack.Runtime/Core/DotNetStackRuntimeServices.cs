using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.Runtime.Core
{
    public class DotNetStackRuntimeServices : IDotNetStackRuntimeServices
    {
        private static readonly object Lock = new object();
        public static IDotNetStackRuntimeServices Current { get; private set; }
        
        public static IDotNetStackRuntimeServices Init(IDotNetStackRuntimeServices runtimeServices)
        {
            if (Current != null) return Current;
            
            lock (Lock)
            {
                return Current ??= runtimeServices;
            }
        }

        public DotNetStackRuntimeServices(IDotNetStackHostBuilder hostBuilder, IDotNetStackApplicationBuilder applicationBuilder, IModuleManager moduleManager, IDiagnosticsCollector diagnostics, RuntimeFlags runtimeFlags)
        {
            HostBuilder = hostBuilder;
            ApplicationBuilder = applicationBuilder;
            ModuleManager = moduleManager;
            Diagnostics = diagnostics;
            Flags = runtimeFlags;
        }

        public IDotNetStackHostBuilder HostBuilder { get; }
        public IDotNetStackApplicationBuilder ApplicationBuilder { get; }
        public IModuleManager ModuleManager { get; }
        public IDiagnosticsCollector Diagnostics { get; }
        public RuntimeFlags Flags { get; }
    }
}
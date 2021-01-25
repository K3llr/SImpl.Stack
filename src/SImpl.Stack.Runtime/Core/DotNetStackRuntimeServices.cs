using SImpl.NanoContainer;
using SImpl.Stack.Diagnostics;
using SImpl.Stack.HostBuilders;

namespace SImpl.Stack.Runtime.Core
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

        public DotNetStackRuntimeServices(INanoContainer container, IDotNetStackHostBuilder hostBuilder, IModuleManager moduleManager, IDiagnosticsCollector diagnostics, RuntimeFlags runtimeFlags)
        {
            BootContainer = container;
            HostBuilder = hostBuilder;
            ModuleManager = moduleManager;
            Diagnostics = diagnostics;
            Flags = runtimeFlags;
        }

        public INanoContainer BootContainer { get; }
        public IDotNetStackHostBuilder HostBuilder { get; }
        public IModuleManager ModuleManager { get; }
        public IDiagnosticsCollector Diagnostics { get; }
        public RuntimeFlags Flags { get; }
    }
}
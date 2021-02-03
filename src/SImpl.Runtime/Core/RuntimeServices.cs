using SImpl.Host.Builders;
using SImpl.NanoContainer;
using SImpl.Runtime.Diagnostics;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime.Core
{
    public class RuntimeServices : IRuntimeServices
    {
        private static readonly object Lock = new object();
        public static IRuntimeServices Current { get; private set; }
        
        public static IRuntimeServices Init(IRuntimeServices runtimeServices)
        {
            if (Current != null) return Current;
            
            lock (Lock)
            {
                return Current ??= runtimeServices;
            }
        }

        public RuntimeServices(INanoContainer container, ISImplHostBuilder hostBuilder, IModuleManager moduleManager, IDiagnosticsCollector diagnostics, RuntimeFlags runtimeFlags)
        {
            BootContainer = container;
            HostBuilder = hostBuilder;
            ModuleManager = moduleManager;
            Diagnostics = diagnostics;
            Flags = runtimeFlags;
        }

        public INanoContainer BootContainer { get; }
        public ISImplHostBuilder HostBuilder { get; }
        public IModuleManager ModuleManager { get; }
        public IDiagnosticsCollector Diagnostics { get; }
        public RuntimeFlags Flags { get; }
    }
}
using SImpl.NanoContainer;
using SImpl.Stack.Diagnostics;
using SImpl.Stack.HostBuilders;

namespace SImpl.Stack.Runtime.Core
{
    public interface IDotNetStackRuntimeServices
    {
        INanoContainer BootContainer { get; }
        IDotNetStackHostBuilder HostBuilder { get; }
        IModuleManager ModuleManager { get; }
        IDiagnosticsCollector Diagnostics { get; }
        RuntimeFlags Flags { get; }
    }
}
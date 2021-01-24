using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.HostBuilders;
using SImpl.NanoContainer;

namespace SImpl.DotNetStack.Runtime.Core
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
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.Runtime.Core
{
    public interface IDotNetStackRuntimeServices
    {
        IDotNetStackHostBuilder HostBuilder { get; }
        IDotNetStackApplicationBuilder ApplicationBuilder { get; }
        IModuleManager ModuleManager { get; }
        IDiagnosticsCollector Diagnostics { get; }
        RuntimeFlags Flags { get; }
    }
}
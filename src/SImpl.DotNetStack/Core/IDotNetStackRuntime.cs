using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Diagnostics;

namespace SImpl.DotNetStack.Core
{
    public interface IDotNetStackRuntime
    {
        IModuleManager ModuleManager { get; }
        IHostBuilder HostBuilder { get; }
        IDiagnosticsCollector Diagnostics { get; }
        RuntimeFlags Flags { get; }
    }
}
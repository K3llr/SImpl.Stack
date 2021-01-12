using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.Diagnostics;
using SImpl.DotNetStack.NanoDependencyInjection;

namespace SImpl.DotNetStack.Core
{
    public interface IDotNetStackRuntime
    {
        INanoContainer Container { get; }
        IModuleManager ModuleManager { get; }
        IHostBuilder HostBuilder { get; }
        IDiagnosticsCollector Diagnostics { get; }
        RuntimeFlags Flags { get; }
    }
}
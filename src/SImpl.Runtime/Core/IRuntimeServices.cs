using SImpl.Host.Builders;
using SImpl.NanoContainer;
using SImpl.Runtime.Diagnostics;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime.Core
{
    public interface IRuntimeServices
    {
        INanoContainer BootContainer { get; }
        ISImplHostBuilder HostBuilder { get; }
        IModuleManager ModuleManager { get; }
        IDiagnosticsCollector Diagnostics { get; }
        RuntimeFlags Flags { get; }
    }
}
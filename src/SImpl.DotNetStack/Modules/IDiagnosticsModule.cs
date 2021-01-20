using SImpl.DotNetStack.Diagnostics;

namespace SImpl.DotNetStack.Modules
{
    public interface IDiagnosticsModule : IDotNetStackModule
    {
        void Diagnose(IDiagnosticsCollector collector);
    }
}
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Diagnostics
{
    public interface IDiagnosticsModule : IDotNetStackModule
    {
        void Diagnose(IDiagnosticsCollector collector);
    }
}
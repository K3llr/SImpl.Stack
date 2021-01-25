using SImpl.Stack.Modules;

namespace SImpl.Stack.Diagnostics
{
    public interface IDiagnosticsModule : IDotNetStackModule
    {
        void Diagnose(IDiagnosticsCollector collector);
    }
}
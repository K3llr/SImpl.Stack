using SImpl.Modules;

namespace SImpl.Runtime.Diagnostics
{
    public interface IDiagnosticsModule : ISImplModule
    {
        void Diagnose(IDiagnosticsCollector collector);
    }
}
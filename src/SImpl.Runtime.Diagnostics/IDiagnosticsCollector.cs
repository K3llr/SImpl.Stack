using System.Collections.Generic;

namespace SImpl.Runtime.Diagnostics
{
    public interface IDiagnosticsCollector
    {
        void AddSection(string key, IDiagnosticsSection section);
        IDiagnosticsSection Get(string key);
        IReadOnlyList<IDiagnosticsSection> Sections { get; }
        void RegisterLapTime(string value);
        IReadOnlyList<LapTime> Timetable { get; }
        void ClearTimetable();
    }
}
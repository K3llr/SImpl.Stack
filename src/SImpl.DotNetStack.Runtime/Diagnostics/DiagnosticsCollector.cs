using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SImpl.DotNetStack.Diagnostics;

namespace SImpl.DotNetStack.Runtime.Diagnostics
{
    public class DiagnosticsCollector : IDiagnosticsCollector
    {
        private readonly IDictionary<string, IDiagnosticsSection> _sections = new Dictionary<string, IDiagnosticsSection>();
        private readonly List<LapTime> _lapTimes = new List<LapTime>();
        private readonly Stopwatch _stopwatch;
        
        public static DiagnosticsCollector Create()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            return new DiagnosticsCollector(stopwatch);
        }

        public DiagnosticsCollector(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }
        
        public void AddSection(string key, IDiagnosticsSection section)
        {
            _sections[key] = section;
        }

        public IDiagnosticsSection Get(string key)
        {
            return _sections.ContainsKey(key)
                ? _sections[key]
                : null;
        }

        public IReadOnlyList<IDiagnosticsSection> Sections => _sections.Values.ToList().AsReadOnly();

        public void RegisterLapTime(string value)
        {
            _lapTimes.Add(new LapTime(value, _stopwatch.Elapsed));
        }

        public IReadOnlyList<LapTime> Timetable => _lapTimes.AsReadOnly();
        public void ClearTimetable()
        {
            _lapTimes.Clear();
        }
    }
}
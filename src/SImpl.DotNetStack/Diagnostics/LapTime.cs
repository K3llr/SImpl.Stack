using System;

namespace SImpl.DotNetStack.Diagnostics
{
    public class LapTime
    {
        public LapTime(string name, TimeSpan elapsed)
        {
            At = DateTime.Now;
            Name = name;
            Elapsed = elapsed;
        }
        public string Name { get; private set; }
        public TimeSpan Elapsed { get; private set; }

        public DateTime At { get; set; }
    }
}
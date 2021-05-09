using System;
using SImpl.Modules;

namespace SImpl.AutoRun
{
    public class AutoRunModuleInfo
    {
        public Type ModuleType { get; set; }
        public Func<ISImplModule> ModuleFactory { get; set; }
    }
}
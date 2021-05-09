using System.Collections.Generic;

namespace SImpl.AutoRun
{
    public interface IAutoRunDiscoveryStrategy
    {
        IEnumerable<AutoRunModuleInfo> Discover();
    }
}
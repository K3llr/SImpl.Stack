using System.Collections.Generic;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    public interface IBootSequence : IEnumerable<ISImplModule>
    {
        IEnumerable<TModule> FilterBy<TModule>()
            where TModule : ISImplModule;

        IBootSequence Reverse();
    }
}
using System.Collections.Generic;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Runtime.Core
{
    public interface IBootSequence : IEnumerable<IDotNetStackModule>
    {
        IEnumerable<TModule> FilterBy<TModule>()
            where TModule : IDotNetStackModule;

        IBootSequence Reverse();
    }
}
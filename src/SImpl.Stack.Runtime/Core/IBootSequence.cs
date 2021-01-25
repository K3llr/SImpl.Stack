using System.Collections.Generic;
using SImpl.Stack.Modules;

namespace SImpl.Stack.Runtime.Core
{
    public interface IBootSequence : IEnumerable<IDotNetStackModule>
    {
        IEnumerable<TModule> FilterBy<TModule>()
            where TModule : IDotNetStackModule;

        IBootSequence Reverse();
    }
}
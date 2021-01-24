using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Runtime.Verbosity
{
    public class VerboseBootSequence : IBootSequence
    {
        private readonly IBootSequence _bootSequence;

        public VerboseBootSequence(IBootSequence bootSequence)
        {
            _bootSequence = bootSequence;
        }

        public IEnumerable<TModule> FilterBy<TModule>() 
            where TModule : IDotNetStackModule
        {
            return _bootSequence.FilterBy<TModule>().Select(ModuleLoggingDecorator<TModule>.Create);
        }

        public IBootSequence Reverse()
        {
            return new VerboseBootSequence(_bootSequence.Reverse());
        }

        public IEnumerator<IDotNetStackModule> GetEnumerator()
        {
            return _bootSequence.Select(ModuleLoggingDecorator<IDotNetStackModule>.Create).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
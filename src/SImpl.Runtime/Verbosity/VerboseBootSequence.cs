using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Verbosity
{
    public class VerboseBootSequence : IBootSequence
    {
        private readonly IBootSequence _bootSequence;

        public VerboseBootSequence(IBootSequence bootSequence)
        {
            _bootSequence = bootSequence;
        }

        public IEnumerable<TModule> FilterBy<TModule>() 
            where TModule : ISImplModule
        {
            return _bootSequence.FilterBy<TModule>().Select(ModuleLoggingDecorator<TModule>.Create);
        }

        public IBootSequence Reverse()
        {
            return new VerboseBootSequence(_bootSequence.Reverse());
        }

        public IEnumerator<ISImplModule> GetEnumerator()
        {
            return _bootSequence.Select(ModuleLoggingDecorator<ISImplModule>.Create).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SImpl.Modules;

namespace SImpl.Runtime.Core
{
    public class BootSequence : IBootSequence
    {
        private readonly IEnumerable<ISImplModule> _bootSequence;

        public BootSequence(IEnumerable<ISImplModule> bootSequence)
        {
            _bootSequence = bootSequence;
        }

        public IEnumerable<TModule> FilterBy<TModule>() 
            where TModule : ISImplModule
        {
            foreach (var module in _bootSequence)
            {
                if (module is TModule typedModule)
                {
                    yield return typedModule;
                }
            }
        }

        public IBootSequence Reverse()
        {
            return new BootSequence(_bootSequence.Reverse());
        }

        public IEnumerator<ISImplModule> GetEnumerator()
        {
            return _bootSequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
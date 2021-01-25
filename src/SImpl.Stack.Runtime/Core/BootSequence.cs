using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SImpl.Stack.Modules;

namespace SImpl.Stack.Runtime.Core
{
    public class BootSequence : IBootSequence
    {
        private readonly IEnumerable<IDotNetStackModule> _bootSequence;

        public BootSequence(IEnumerable<IDotNetStackModule> bootSequence)
        {
            _bootSequence = bootSequence;
        }

        public IEnumerable<TModule> FilterBy<TModule>() 
            where TModule : IDotNetStackModule
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

        public IEnumerator<IDotNetStackModule> GetEnumerator()
        {
            return _bootSequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Runtime.Verbosity
{
    public class VerboseBootSequenceFactory : IBootSequenceFactory
    {
        private readonly IBootSequenceFactory _bootSequenceFactory;

        public VerboseBootSequenceFactory(IBootSequenceFactory bootSequenceFactory)
        {
            _bootSequenceFactory = bootSequenceFactory;
        }

        public IBootSequence New()
        {
            return new VerboseBootSequence(_bootSequenceFactory.New());
        }
    }
}
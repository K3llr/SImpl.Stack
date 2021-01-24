using SImpl.DotNetStack.Runtime.Core;

namespace SImpl.DotNetStack.Runtime.Verbosity
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
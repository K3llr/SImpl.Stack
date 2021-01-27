using SImpl.NanoContainer;
using SImpl.Runtime.Core;
using SImpl.Runtime.Host.Builders;

namespace SImpl.Runtime.Verbosity
{
    public static class NanoContainerVerbosityExtensions
    {
        public static INanoContainer AddVerbosity(this INanoContainer container)
        {
            container.RegisterDecorator<IModuleManager, VerboseModuleManager>();
            container.RegisterDecorator<IBootSequenceFactory, VerboseBootSequenceFactory>();
            container.RegisterDecorator<IHostBootManager, VerboseHostBootManager>();
            container.RegisterDecorator<ISImplHostBuilder, VerboseHostBuilder>();
            container.RegisterDecorator<IApplicationBootManager, VerboseApplicationBootManager>();
            
            return container;
        }
    }
}
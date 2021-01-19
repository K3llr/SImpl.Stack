using SImpl.DotNetStack.Application;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Core;

namespace SImpl.DotNetStack.GenericHost.ApplicationBuilders
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        public ApplicationBuilder(IDotNetStackRuntime runtime)
        {
            Runtime = runtime;
        }
        
        public IDotNetStackRuntime Runtime { get; }
        
        public IDotNetStackApplication Build()
        {
            return Runtime.Container.Resolve<IDotNetStackApplicationBuilder>().Build();
        }
    }
}
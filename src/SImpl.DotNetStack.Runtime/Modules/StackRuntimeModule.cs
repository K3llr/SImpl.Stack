using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.NanoContainer;

namespace SImpl.DotNetStack.Runtime.Modules
{
    public class StackRuntimeModule : IServicesCollectionConfigureModule
    {
        private readonly INanoContainer _bootContainer;

        public StackRuntimeModule(INanoContainer bootContainer)
        {
            _bootContainer = bootContainer;
        }
        public string Name => nameof(StackRuntimeModule);
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_bootContainer);
            services.AddSingleton(_bootContainer.Resolve<IBootSequenceFactory>());
        }
    }
}
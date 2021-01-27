using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;
using SImpl.NanoContainer;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Modules
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
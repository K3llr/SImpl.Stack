using Microsoft.Extensions.DependencyInjection;
using Simpl.DependencyInjection;
using SImpl.Modules;

namespace SImpl.Queue.Module
{
    public class DependencyInjectionModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(DependencyInjectionModule);
        public void ConfigureServices(IServiceCollection services)
        {  
            services.AddSingleton<IContainerService, ContainerService>();
        }
        
        public DependencyInjectionModule(DependencyInjectionConfig config)
        {
        }
    }
}
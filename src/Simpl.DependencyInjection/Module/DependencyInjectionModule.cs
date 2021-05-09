using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace Simpl.DependencyInjection.Module
{
    public class DependencyInjectionModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(DependencyInjectionModule);
        public void ConfigureServices(IServiceCollection services)
        {  
            services.AddSingleton<IContainerService, ContainerService>();
            new ContainerRegisterService(services);
        }
        
        public DependencyInjectionModule(DependencyInjectionConfig config)
        {
        }
    }
}
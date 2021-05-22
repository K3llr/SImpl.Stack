using Microsoft.Extensions.DependencyInjection;
using SImpl.Modules;

namespace Simpl.DependencyInjection.Module
{
    public class DependencyInjectionModule : IServicesCollectionConfigureModule
    {
        public string Name { get; } = nameof(DependencyInjectionModule);
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddSingleton<IContainerService, ContainerService>(provider => new ContainerService(provider));
            foreach (var dependency in ContainerRegisterService.Current.Dependencies)
            {
                switch (dependency.Lifestyle)
                {
                    case Lifestyle.Scoped:
                    
                        services.AddScoped(dependency.Abstraction, dependency.Implementation);
                        break;
                    case Lifestyle.Singleton:
                        services.AddSingleton(dependency.Abstraction, dependency.Implementation);
                        break;
                    case Lifestyle.Transient:
                        services.AddTransient(dependency.Abstraction, dependency.Implementation);
                        break;
                }  
            }
         
        }
        
        public DependencyInjectionModule(DependencyInjectionConfig config)
        {
        }
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace Simpl.DependencyInjection
{
    internal class ContainerRegisterService : IContainerRegisterService
    {
        private readonly IServiceCollection _serviceCollection;

        internal ContainerRegisterService(IServiceCollection  serviceCollection)
        {
            _serviceCollection = serviceCollection;

            Current = this;
        }
        public void Register<TAbstraction, TImplementation>(Dependency<TAbstraction, TImplementation> dependency) where TAbstraction : class where TImplementation : class, TAbstraction
        {
            switch (dependency.Lifestyle)
            {
                case Lifestyle.Scoped:
                    
                    _serviceCollection.AddScoped(dependency.Abstraction, dependency.Implementation);
                    break;
                case Lifestyle.Singleton:
                    _serviceCollection.AddSingleton(dependency.Abstraction, dependency.Implementation);
                    break;
                case Lifestyle.Transient:
                    _serviceCollection.AddTransient(dependency.Abstraction, dependency.Implementation);
                    break;
            }
        }

        public static ContainerRegisterService Current;

    }
}
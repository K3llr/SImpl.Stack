using Microsoft.Extensions.DependencyInjection;
using SImpl.DependencyInjection.Models;
using SImpl.DependencyInjection.Services;

namespace SImpl.DependencyInjection.Factories
{
    public class ContainerFactory
    {
        internal static void RegisterDependency<TAbstraction, TImplementation>(Dependency<TAbstraction, TImplementation> dependency)
            where TImplementation : class, TAbstraction
            where TAbstraction : class
        {
            ContainerRegisterService.Current.Register(dependency);
        }

        public static ContainerFactory<TAbstraction, TImplementation> New<TAbstraction, TImplementation>()
            where TImplementation : class, TAbstraction
            where TAbstraction : class
        {
            return new ContainerFactory<TAbstraction, TImplementation>();
        }

        public static ContainerFactory<TImplementation> New<TImplementation>()
            where TImplementation : class
        {
            return new ContainerFactory<TImplementation>();
        }
        
    }

    public class ContainerFactory<TImplementation> : ContainerFactory<TImplementation, TImplementation>
        where TImplementation : class
    {
        internal ContainerFactory() : base()
        {
        }
    }

    public class ContainerFactory<TAbstraction, TImplementation> : IContainerFactory<TAbstraction>, IContainerAware
        where TImplementation : class, TAbstraction
        where TAbstraction : class
    {
        internal ContainerFactory()
        {
            var dependency = new Dependency<TAbstraction, TImplementation>
            {
                Abstraction = typeof(TAbstraction),
                Implementation = typeof(TImplementation),
                Lifestyle = Lifestyle.Scoped
            };

            ContainerFactory.RegisterDependency(dependency);
        }

        public TAbstraction New()
        {
            return this.Resolve<TAbstraction>();
        }

        public TAbstraction New(IServiceScope scope)
        {
            return this.Resolve<TAbstraction>(scope);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Simpl.DependencyInjection;
using SImpl.Factories;

namespace Novicell.App.Headless.Core.Factories
{
    public class SingletonContainerFactory<TImplementation> : SingletonContainerFactory<TImplementation, TImplementation>
        where TImplementation : class
    {
        internal SingletonContainerFactory() : base()
        {
        }
    }

    public class SingletonContainerFactory<TAbstraction, TImplementation> : IContainerFactory<TAbstraction>, IContainerAware
        where TImplementation : class, TAbstraction
        where TAbstraction : class
    {
        internal SingletonContainerFactory()
        {
            var dependency = new Dependency<TAbstraction, TImplementation>
            {
                Abstraction = typeof(TAbstraction),
                Implementation = typeof(TImplementation),
                Lifestyle = Lifestyle.Singleton
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

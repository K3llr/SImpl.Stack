using Microsoft.Extensions.DependencyInjection;
using Simpl.DependencyInjection.Models;
using Simpl.DependencyInjection.Services;

namespace Simpl.DependencyInjection.Factories
{
    public class SingletonContainerFactory
    {
        internal static void RegisterDependency<TAbstraction, TImplementation>(Dependency<TAbstraction, TImplementation> dependency)
            where TImplementation : class, TAbstraction
            where TAbstraction : class
        {
            ContainerRegisterService.Current.Register(dependency);
        }

        public static SingletonContainerFactory<TAbstraction, TImplementation> New<TAbstraction, TImplementation>()
            where TImplementation : class, TAbstraction
            where TAbstraction : class
        {
            return new SingletonContainerFactory<TAbstraction, TImplementation>();
        }

        public static SingletonContainerFactory<TImplementation> New<TImplementation>()
            where TImplementation : class
        {
            return new SingletonContainerFactory<TImplementation>();
        }
    }
    
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
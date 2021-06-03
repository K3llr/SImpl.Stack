using Simpl.DependencyInjection;

namespace Novicell.App.Headless.Core.Factories
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
}
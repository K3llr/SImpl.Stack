using SImpl.DependencyInjection.Models;

namespace SImpl.DependencyInjection
{
    public interface IContainerRegisterService
    {
        void Register<TAbstraction, TImplementation>(Dependency<TAbstraction, TImplementation> dependency)
            where TAbstraction : class where TImplementation : class, TAbstraction;
    }
}
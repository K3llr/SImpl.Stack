using System.Collections.Generic;
using SImpl.DependencyInjection.Models;

namespace SImpl.DependencyInjection.Services
{
    internal class ContainerRegisterService : IContainerRegisterService
    {
        public readonly List<IDependency> Dependencies = new List<IDependency>();

        internal ContainerRegisterService()
        {
            CurrentInstance = this;
        }
        public void Register<TAbstraction, TImplementation>(Dependency<TAbstraction, TImplementation> dependency) where TAbstraction : class where TImplementation : class, TAbstraction
        {
           Dependencies.Add(dependency);
        }

        private static ContainerRegisterService CurrentInstance;
        internal static ContainerRegisterService Current =CurrentInstance ?? new ContainerRegisterService();

    }
}
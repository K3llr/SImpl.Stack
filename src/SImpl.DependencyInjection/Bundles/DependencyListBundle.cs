using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DependencyInjection.Exceptions;
using SImpl.DependencyInjection.Models;

namespace SImpl.DependencyInjection.Bundles
{
    public class DependencyListBundle : IDependencyInjectionBundle
    {
        private readonly IEnumerable<IDependency> _dependencies;

        public DependencyListBundle(IEnumerable<IDependency> dependencies)
        {
            _dependencies = dependencies;
        }

        public void Configure(IServiceCollection serviceCollection)
        {
            foreach (var dependency in _dependencies)
            {
                if (dependency.IsInvalid())
                    throw new InvalidDependencyException(dependency.ErrorMessage, dependency);
                switch (dependency.Type)
                {
                    case DependencyType.Decorator:
                        serviceCollection.Decorate(dependency.Abstraction,dependency.Implementation);
                        break;
                    case DependencyType.Default:
                    default:
                        serviceCollection.AddScoped(dependency.Implementation);
                        break;
                }
                
            }
        }
    }
}
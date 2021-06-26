using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace SImpl.DependencyInjection.Bundles
{
    public class DependencyCollectionListBundle : IDependencyInjectionBundle
    {
        private readonly IEnumerable<IEnumerable<IDependency>> _dependencyCollections;

        public DependencyCollectionListBundle(IEnumerable<IEnumerable<IDependency>> dependencyCollections)
        {
            _dependencyCollections = dependencyCollections;
        }

        public void Configure(IServiceCollection serviceCollection)
        {
            foreach (var dependencyCollection in _dependencyCollections)
            {
                Type abstraction = null;
                var dependencyList = new List<Type>();
                foreach (var dependency in dependencyCollection)
                {
                    serviceCollection.AddScoped(dependency.Implementation);
                    abstraction = dependency.Abstraction;
                    dependencyList.Add(dependency.Implementation);
                }

                if (abstraction != null)
                {
                    foreach (var depedency in dependencyList)
                    {
                        serviceCollection.AddScoped(abstraction, depedency);
                    }
                }
            }
        }
    }
}
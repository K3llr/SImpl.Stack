using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Simpl.DependencyInjection
{
    public class ContainerService : IContainerService
    {
        public static IContainerService Current { get; private set; }
        public IServiceProvider  ServiceProvider { get; }

        public ContainerService(IServiceProvider  serviceProvider)
        {
            ServiceProvider = serviceProvider;

            Current = this;
        }

        public T Resolve<T>() where T : class
        {
           return ServiceProvider.GetService<T>();
        }

        public T Resolve<T>(Type t) where T : class
        {
            return ServiceProvider.GetService(t) as T;
        }

        public T Resolve<T>(IServiceScope scope) where T : class
        {
           return scope.ServiceProvider.GetService<T>();
        }
        public T Resolve<T>(IServiceScope scope, Type t) where T : class
        {
            return scope.ServiceProvider.GetService(t) as T;
        }
        public IList<T> ResolveCollection<T>() where T : class
        {
            return ResolveCollection<T>(typeof(T));
        }

        public IList<T> ResolveCollection<T>(Type t) where T : class
        {
            return ServiceProvider.GetServices(t).Cast<T>().ToList();
        }

        public bool IsRegistered<T>()
        {
            return IsRegistered(typeof(T));
        }

        public bool IsRegistered(Type t)
        {
            return ServiceProvider.GetService(t) != null;
        }

        public IServiceScope BeginScope()
        {
            return ServiceProvider.CreateScope();
        }

    }
}
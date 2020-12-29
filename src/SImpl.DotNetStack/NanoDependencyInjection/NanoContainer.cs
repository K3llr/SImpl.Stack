using System;
using System.Collections.Generic;

namespace SImpl.DotNetStack.NanoDependencyInjection
{
    public class NanoContainer : INanoContainer
    {
        private readonly IDictionary<Type, INanoServiceRegistration> _instances = new Dictionary<Type, INanoServiceRegistration>();
        private bool _hasResolved = false;
        
        public INanoContainer Register<TService, TImplementation>()
            where TImplementation : TService
        {
            return AddServiceRegistration(typeof(TService), new TypedServiceRegistration<TImplementation>(Resolve));
        }
        
        public INanoContainer Register<TService>(Func<TService> factory)
        {
            return AddServiceRegistration(typeof(TService), new FactoryServiceRegistration<TService>(factory));
        }

        public INanoContainer Register<TService>(TService instance)
        {
            return Register(() => instance);;
        }

        public INanoContainer RegisterDecorator<TService, TDecorator>() 
            where TDecorator : TService
        {
            var key = typeof(TService);

            if (!_instances.ContainsKey(key))
            {
                throw new Exception($"NanoContainer is unable to decorate service {typeof(TService).FullName} with {typeof(TDecorator).FullName}");
            }

            
            var currentServiceRegistration = _instances[key];
            AddServiceRegistration(typeof(TDecorator), new TypedServiceRegistration<TDecorator>(
                t =>
                    typeof(TService).IsAssignableFrom(t)
                        ? currentServiceRegistration.Resolve()
                        : Resolve(t)));
            
            AddServiceRegistration(key, new DecoratorServiceRegistration<TDecorator>(Resolve), true);

            return this;
        }

        public TService Resolve<TService>()
        {
            return (TService)Resolve(typeof(TService));
        }

        public object Resolve(Type serviceType)
        {
            _hasResolved = true;
            
            if (!_instances.ContainsKey(serviceType))
            {
                throw new Exception($"NanoContainer is unable to resolve type {serviceType.FullName}");
            }
            
            return _instances[serviceType].Resolve();
        }

        private INanoContainer AddServiceRegistration(Type key, INanoServiceRegistration registration, bool overwrite = false)
        {
            if (_hasResolved)
            {
                throw new Exception($"Cannot add new registrations after first Resolve()");
            }
            
            if (!overwrite && _instances.ContainsKey(key))
            {
                throw new Exception($"NanoContainer already contains a registration for type {key.FullName}");
            }

            _instances[key] = registration;

            return this;
        }
    }
}
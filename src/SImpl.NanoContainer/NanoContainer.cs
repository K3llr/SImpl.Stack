using System;
using System.Collections.Generic;
using SImpl.NanoContainer.Registrations;

namespace SImpl.NanoContainer
{
    public class NanoContainer : INanoContainer
    {
        private readonly IDictionary<Type, IServiceResolver> _instances = new Dictionary<Type, IServiceResolver>();
        private bool _allowRegistrations = true;

        public INanoContainer Register<TService>(IServiceResolver serviceResolver)
        {
            return AddServiceRegistration(typeof(TService), serviceResolver);
        }

        public INanoContainer Register<TService, TImplementation>()
            where TImplementation : TService
        {
            return AddServiceRegistration(typeof(TService), new TypedServiceResolver<TImplementation>(Resolve));
        }
        
        public INanoContainer Register<TService>(Func<TService> factory)
        {
            return AddServiceRegistration(typeof(TService), new FactoryServiceResolver<TService>(factory));
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
            AddServiceRegistration(typeof(TDecorator), new TypedServiceResolver<TDecorator>(
                t =>
                    typeof(TService).IsAssignableFrom(t)
                        ? currentServiceRegistration.Resolve()
                        : Resolve(t)));
            
            AddServiceRegistration(key, new DecoratorServiceResolver<TDecorator>(Resolve), true);

            return this;
        }

        public TService Resolve<TService>()
        {
            return (TService)Resolve(typeof(TService));
        }

        public object Resolve(Type serviceType)
        {
            _allowRegistrations = false;
            
            if (!_instances.ContainsKey(serviceType))
            {
                throw new Exception($"NanoContainer is unable to resolve type {serviceType.FullName}");
            }
            
            return _instances[serviceType].Resolve();
        }

        public TService New<TService>()
        {
            return New<TService>(new Dictionary<Type, object>());
        }
        
        public TService New<TService>(IDictionary<Type, object> overrideScope)
        {
            return (TService)new TypedServiceResolver<TService>(key => overrideScope.ContainsKey(key) ? overrideScope[key] : Resolve(key)).Resolve();
        }

        private INanoContainer AddServiceRegistration(Type key, IServiceResolver resolver, bool overwrite = false)
        {
            // TODO: Detect recursive dependency
            
            if (!_allowRegistrations)
            {
                throw new Exception($"Cannot add new registrations after first Resolve()");
            }
            
            if (!overwrite && _instances.ContainsKey(key))
            {
                throw new Exception($"NanoContainer already contains a registration for type {key.FullName}");
            }

            _instances[key] = resolver;

            return this;
        }
    }
}
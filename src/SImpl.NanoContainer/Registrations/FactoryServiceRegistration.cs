using System;

namespace SImpl.NanoContainer.Registrations
{
    public class FactoryServiceRegistration<TService> : INanoServiceRegistration
    {
        private TService _instance;
        private readonly Func<TService> _factory;

        public FactoryServiceRegistration(Func<TService> factory)
        {
            _factory = factory;
        }
        
        public object Resolve()
        {
            return _instance ??= _factory.Invoke();
        }
    }
}
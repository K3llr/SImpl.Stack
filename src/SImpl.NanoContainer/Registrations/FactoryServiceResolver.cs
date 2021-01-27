using System;

namespace SImpl.NanoContainer.Registrations
{
    public class FactoryServiceResolver<TService> : IServiceResolver
    {
        private TService _instance;
        private readonly Func<TService> _factory;

        public FactoryServiceResolver(Func<TService> factory)
        {
            _factory = factory;
        }
        
        public object Resolve()
        {
            return _instance ??= _factory.Invoke();
        }
    }
}
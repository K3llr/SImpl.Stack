using System;

namespace SImpl.NanoContainer.Registrations
{
    public class DecoratorServiceResolver<TDecorator> : IServiceResolver
    {
        private readonly Func<Type, object> _resolver;
        
        private object _instance;

        public DecoratorServiceResolver(Func<Type, object> resolver)
        {
            _resolver = resolver;
        }
        
        public object Resolve()
        {
            return _instance ??= _resolver.Invoke(typeof(TDecorator));
        }
    }
}
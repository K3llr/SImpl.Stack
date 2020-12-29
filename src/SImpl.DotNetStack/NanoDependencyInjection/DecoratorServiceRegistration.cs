using System;

namespace SImpl.DotNetStack.DI
{
    public class DecoratorServiceRegistration<TDecorator> : INanoServiceRegistration
    {
        private readonly Func<Type, object> _resolver;
        
        private object _instance;

        public DecoratorServiceRegistration(Func<Type, object> resolver)
        {
            _resolver = resolver;
        }
        
        public object Resolve()
        {
            return _instance ??= _resolver.Invoke(typeof(TDecorator));
        }
    }
}
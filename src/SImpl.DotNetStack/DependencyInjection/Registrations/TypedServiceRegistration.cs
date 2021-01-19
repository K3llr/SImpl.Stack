using System;
using System.Linq;
using System.Reflection;

namespace SImpl.DotNetStack.DependencyInjection.Registrations
{
    public class TypedServiceRegistration<TImplementation> : INanoServiceRegistration
    {
        private readonly Func<Type, object> _resolver;
        private readonly ConstructorInfo _ctor;
        
        private object _instance;
        
        public TypedServiceRegistration(Func<Type, object> resolver)
        {
            _resolver = resolver;

            try
            {
                _ctor = typeof(TImplementation).GetConstructors().Single();
            }
            catch (Exception)
            {
                throw new Exception($"Services is required to have a single constructor {typeof(TImplementation).FullName}");
            }
        }
        
        public object Resolve()
        {
            return _instance ??= _ctor.Invoke(_ctor
                .GetParameters()
                .Select(p => _resolver.Invoke(p.ParameterType))
                .ToArray());
        }
    }
}
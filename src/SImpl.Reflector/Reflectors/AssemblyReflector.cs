using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SImpl.Reflect.Reflectors
{
    public class AssemblyReflector
    {
        private Assembly[] _assemblies;

        public AssemblyReflector(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<TypeAttributeReflector<TAttribute>> TypesMarkedByAttribute<TAttribute>(bool inherit = false)
            where TAttribute : Attribute
        {
            return _assemblies
                .SelectMany(
                    assembly => assembly.GetTypes(), 
                    (assembly, type) => type)
                .Where(type => type.IsDefined(typeof(TAttribute), inherit))
                .Select(type => new TypeAttributeReflector<TAttribute>
                {
                    ReflectedType = type,
                    Attribute = type.GetCustomAttribute<TAttribute>()
                });
        }
        
        public IEnumerable<TypeReflector> TypesMarkedByInterface<TInterface>()
        {
            return _assemblies
                .SelectMany(
                    assembly => assembly.GetTypes(), 
                    (assembly, type) => type)
                .Where(type => type.IsAssignableTo(typeof(TInterface)))
                .Select(type => new TypeReflector
                {
                    ReflectedType = type,
                });
        }

        public AssemblyReflector And(params Assembly[] assemblies)
        {
            _assemblies = _assemblies.Union(assemblies).Distinct().ToArray();
            return this;
        }
        
    }
}
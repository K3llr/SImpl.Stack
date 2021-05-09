using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SImpl.Reflect.Reflectors;

namespace SImpl.Reflect
{
    public static class Reflect
    {
        public static AssemblyReflector AssemblyOf<T>()
        {
            return new AssemblyReflector(typeof(T).Assembly);
        }
        
        public static AssemblyReflector AssemblyOf(Type type)
        {
            return new AssemblyReflector(type.Assembly);
        }
        
        public static AssemblyReflector AssembliesOf(Type[] types)
        {
            return new AssemblyReflector(
                types
                    .Select(t => t.Assembly)
                    .Distinct()
                    .ToArray());
        }
        
        public static AssemblyReflector Assemblies(IEnumerable<Assembly> assemblies)
        {
            return new AssemblyReflector(assemblies.ToArray());
        }

        public static AssemblyReflector AssembliesMarkedByAttribute<TAttribute>(IEnumerable<Assembly> assemblies)
            where TAttribute : Attribute
        {
            var autoDiscoveredAssemblies = new List<Assembly>();

            foreach (var assembly in assemblies)
            {
                if (Attribute.GetCustomAttribute(assembly, typeof(TAttribute)) != null)
                {
                    autoDiscoveredAssemblies.Add(assembly);
                }
            }

            return new AssemblyReflector(autoDiscoveredAssemblies.ToArray());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using SImpl.Factories;
using SImpl.Modules;
using SImpl.Reflect;

namespace SImpl.AutoRun.Services
{
    public class ReflectionAutoRunDiscoveryStrategy: IAutoRunDiscoveryStrategy
    {
        private readonly IAssemblyProvider _assemblyProvider;

        public ReflectionAutoRunDiscoveryStrategy(IAssemblyProvider assemblyProvider)
        {
            _assemblyProvider = assemblyProvider;
        }
        
        public IEnumerable<AutoRunModuleInfo> Discover()
        {
            var assemblies = _assemblyProvider.GetAssemblies();
            
            return Reflect.Reflect
                .AssembliesMarkedByAttribute<AutoRunDiscoverableAttribute>(assemblies.ToArray())
                .TypesMarkedByAttribute<AutoRunAttribute>()
                .Select(type => new AutoRunModuleInfo
                {
                    ModuleType = type.ReflectedType,
                    ModuleFactory = () =>
                    {
                        var factoryType = type.Attribute.FactoryType;
                        var factory = factoryType?.GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });

                        if (factory is IModuleFactory moduleFactory)
                        {
                            return moduleFactory.New();
                        }

                        try
                        {
                            if (Activator.CreateInstance(type.ReflectedType) is ISImplModule sImplModule)
                            {
                                return sImplModule;
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }

                        throw new Exception(                                                                                                                              
                            $"The AutoRunModule: '{type.ReflectedType.FullName}' is required to either contain a parameterless constructor or implement IModuleFactory ");
                    }
                });
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper.Contrib.Extensions;
using SImpl.Common;

namespace SImpl.Storage.Repository.Dapper
{
    public class TypeMapper
    {
        public static TypeMapper Mappings
        {
            get
            {
                SqlMapperExtensions.TableNameMapper = new SqlMapperExtensions.TableNameMapperDelegate(TableNameMapper);
                SqlMapperExtensions.KeyPropertiesMapper = KeyPropertiesMapper;
                SqlMapperExtensions.ExplicitKeyPropertiesMapper = ExplicitKeyPropertiesMapper;
                SqlMapperExtensions.ComputedPropertiesMapper = ComputedPropertiesMapper;
                
                return new();
            } 
        }

        private static readonly IDictionary<RuntimeTypeHandle, TypeMap> MappingLookup =
            new ConcurrentDictionary<RuntimeTypeHandle, TypeMap>();
        
        public TypeMapper Map<T>(Action<TypeMap> typeMapping)
            where T : IEntity
        {
            var typeMap = new TypeMap();
            typeMapping(typeMap);
            
            MappingLookup[typeof(T).TypeHandle] = typeMap;
            
            return this;
        }

        private static TypeMap GetTypeMap(Type type)
        {
            if (MappingLookup.TryGetValue(type.TypeHandle, out TypeMap typeMap))
            {
                return typeMap;
            }
            else
            {
                throw new ApplicationException($"{type.Name} is not mapped");
            }
        }

        private static readonly Func<Type, TypeMap, Func<TypeMap, string[]>, List<PropertyInfo>> PropertyMappingFilter = 
            (type, typeMap, selector) => type.GetProperties().Where(p => selector(typeMap).Contains(p.Name)).ToList();

        private static readonly Func<Type, List<PropertyInfo>> KeyPropertiesMapper =
            type => PropertyMappingFilter(type, GetTypeMap(type), m => m.Keys);

        private static readonly Func<Type, List<PropertyInfo>> ExplicitKeyPropertiesMapper =
            type => PropertyMappingFilter(type, GetTypeMap(type), m => m.ExplicitKeys);

        private static readonly Func<Type, List<PropertyInfo>> ComputedPropertiesMapper =
            type => PropertyMappingFilter(type, GetTypeMap(type), m => m.ComputedProperties);

        private static readonly Func<Type, string> TableNameMapper =
            type => GetTypeMap(type).TableName;
    }
}
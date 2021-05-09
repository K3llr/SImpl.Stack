using System;

namespace SImpl.Reflect.Reflectors
{
    public class TypeAttributeReflector<TAttribute> : TypeReflector
        where TAttribute : Attribute
    {
        public TAttribute Attribute { get; set; }
    }
}
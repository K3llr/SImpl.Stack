using System;

namespace SImpl.AutoRun
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRunAttribute : Attribute
    {
        public Type FactoryType { get; }

        public AutoRunAttribute(Type factoryType = null)
        {
            FactoryType = factoryType;
        }
    }
}
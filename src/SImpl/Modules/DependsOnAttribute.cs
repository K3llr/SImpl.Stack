using System;

namespace SImpl.Modules
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(params Type[] dependencies)
        {
            Dependencies = dependencies;
        }
        
        public Type[] Dependencies { get; }
    }
}
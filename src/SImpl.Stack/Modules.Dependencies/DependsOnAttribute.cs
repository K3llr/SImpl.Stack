using System;

namespace SImpl.Stack.Modules.Dependencies
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
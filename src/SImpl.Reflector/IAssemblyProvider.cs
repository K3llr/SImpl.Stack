using System.Collections.Generic;
using System.Reflection;

namespace SImpl.Reflect
{
    public interface IAssemblyProvider
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
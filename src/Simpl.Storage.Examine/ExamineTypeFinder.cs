using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Simpl.Storage.Examine
{
    public static class ExamineTypeFinder
    {
        
        private static readonly ConcurrentDictionary<string, Type> TypeNamesCache = new ConcurrentDictionary<string, Type>();
         public static Type GetTypeByName(string name)
                {
                    
                    //NOTE: This will not find types in dynamic assemblies unless those assemblies are already loaded
                    //into the appdomain.


                    // This is exactly what the BuildManager does, if the type is an assembly qualified type
                    // name it will find it.
                    if (TypeNameContainsAssembly(name))
                    {
                        return Type.GetType(name);
                    }

                    // It didn't parse, so try loading from each already loaded assembly and cache it
                    return TypeNamesCache.GetOrAdd(name, s =>
                        AppDomain.CurrentDomain.GetAssemblies()
                            .Select(x => x.GetType(s))
                            .FirstOrDefault(x => x != null));
                }
         // borrowed from aspnet System.Web.UI.Util
         private static bool TypeNameContainsAssembly(string typeName)
         {
             return CommaIndexInTypeName(typeName) > 0;
         }

         // borrowed from aspnet System.Web.UI.Util
         private static int CommaIndexInTypeName(string typeName)
         {
             var num1 = typeName.LastIndexOf(',');
             if (num1 < 0)
                 return -1;
             var num2 = typeName.LastIndexOf(']');
             if (num2 > num1)
                 return -1;
             return typeName.IndexOf(',', num2 + 1);
         }
    }
}
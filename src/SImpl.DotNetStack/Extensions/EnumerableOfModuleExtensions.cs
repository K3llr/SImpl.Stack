using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TModule> FilterBy<TModule>(this IEnumerable<IDotNetStackModule> modules)
            where TModule : IDotNetStackModule
        {
            foreach (var module in modules)
            {
                if (module is TModule typedModule)
                {
                    yield return typedModule;
                }
            }
        }
        
        public static void ForEach<TModule>(this IEnumerable<IDotNetStackModule> modules, Action<TModule> action)
            where TModule : IDotNetStackModule
        {
            foreach (var module in modules.FilterBy<TModule>())
            {
                action.Invoke(module);
            }
        }
        
        public static async Task ForEachAsync<TModule>(this IEnumerable<IDotNetStackModule> modules, Func<TModule, Task> action)
            where TModule : IDotNetStackModule
        {
            foreach (var module in modules.FilterBy<TModule>())
            {
                await action.Invoke(module);
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.Core;

namespace SImpl.Stack.Runtime.Extensions
{
    public static class BootSequenceExtensions
    {
        public static void ForEach<TModule>(this IBootSequence bootSequence, Action<TModule> action)
            where TModule : IDotNetStackModule
        {
            foreach (var module in bootSequence.FilterBy<TModule>())
            {
                action.Invoke(module);
            }
        }
        
        public static async Task ForEachAsync<TModule>(this IBootSequence bootSequence, Func<TModule, Task> action)
            where TModule : IDotNetStackModule
        {
            foreach (var module in bootSequence.FilterBy<TModule>())
            {
                await action.Invoke(module);
            }
        }
    }
}
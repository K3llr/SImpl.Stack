using System;
using System.Threading.Tasks;
using SImpl.Modules;
using SImpl.Runtime.Core;

namespace SImpl.Runtime.Extensions
{
    public static class BootSequenceExtensions
    {
        public static void ForEach<TModule>(this IBootSequence bootSequence, Action<TModule> action)
            where TModule : ISImplModule
        {
            foreach (var module in bootSequence.FilterBy<TModule>())
            {
                action.Invoke(module);
            }
        }
        
        public static async Task ForEachAsync<TModule>(this IBootSequence bootSequence, Func<TModule, Task> action)
            where TModule : ISImplModule
        {
            foreach (var module in bootSequence.FilterBy<TModule>())
            {
                await action.Invoke(module);
            }
        }
    }
}
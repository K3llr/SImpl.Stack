using SImpl.DotNetStack.Runtime.Extensions;
using System.Linq;

namespace SImpl.DotNetStack.Runtime.Core
{
    public class BootSequenceFactory : IBootSequenceFactory
    {
        private readonly IModuleManager _moduleManager;

        public BootSequenceFactory(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }
        
        public IBootSequence New()
        {
            var enabledModuleContexts =
                _moduleManager.ModuleInfos
                    .Where(info => info.IsEnabled)
                    .Select(info => new ModuleContext
                    {
                        Info = info
                    }).ToList();

            var moduleContextLookup =
                enabledModuleContexts
                    .ToDictionary(ctx => ctx.Info.ModuleType);

            foreach (var ctx in enabledModuleContexts)
            {
                ctx.Dependencies = ctx.Info.Dependencies
                    .Select(t => moduleContextLookup.ContainsKey(t) ? moduleContextLookup[t] : null)
                    .Where(d => d is not null)
                    .ToArray();
            }

            var bootSequence = enabledModuleContexts
                .TopologicalSort(ctx => ctx.Dependencies)
                .Select(ctx => ctx.Info.Module)
                .ToList()
                .AsReadOnly();

            return new BootSequence(bootSequence);
        }
        
        private class ModuleContext
        {
            public ModuleRuntimeInfo Info { get; set; }
            public ModuleContext[] Dependencies { get; set; }
        }
    }
}
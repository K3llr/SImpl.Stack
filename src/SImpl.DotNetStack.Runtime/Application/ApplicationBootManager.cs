using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Runtime.Core;
using SImpl.DotNetStack.Runtime.Extensions;

namespace SImpl.DotNetStack.Runtime.Application
{
    public class ApplicationBootManager : IApplicationBootManager
    {
        private readonly IModuleManager _moduleManager;

        public ApplicationBootManager(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        private IEnumerable<IApplicationModule> _bootSequence;
        private IEnumerable<IApplicationModule> BootSequence => _bootSequence ??= _moduleManager.BootSequence.FilterBy<IApplicationModule>();

        public IEnumerable<IApplicationModule> Configure(IDotNetStackApplicationBuilder appBuilder)
        {
            var configuredModules = new List<IApplicationModule>();
            
            // NOTE: Please do no try and optimize. Especially not to put Config.EnabledModules into a variable before the loop or to use an foreach loop
            // because the collection is expanded inside the loop due to recursive nature of the stack.
            for (var i = 0; i < _moduleManager.EnabledModules.Count; i++)
            {
                var module = _moduleManager.EnabledModules[i] as IApplicationModule;
                module?.Configure(appBuilder);
                
                configuredModules.Add(module);
            }

            return configuredModules;
        }

        public async Task StartAsync()
        {
            await BootSequence.ForEachAsync<IApplicationModule>(module => module.StartAsync());
            
            _moduleManager.SetModuleState(ModuleState.Started);
        }

        public async Task StopAsync()
        {
            await BootSequence.Reverse().ForEachAsync<IApplicationModule>(module => module.StopAsync());
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }
    }
}
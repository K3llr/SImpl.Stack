using System.Collections.Generic;
using System.Threading.Tasks;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Modules;
using SImpl.Stack.Runtime.Extensions;

namespace SImpl.Stack.Runtime.Core
{
    public class ApplicationBootManager : IApplicationBootManager
    {
        private readonly IModuleManager _moduleManager;
        private readonly IBootSequenceFactory _bootSequenceFactory;

        public ApplicationBootManager(IModuleManager moduleManager, IBootSequenceFactory bootSequenceFactory)
        {
            _moduleManager = moduleManager;
            _bootSequenceFactory = bootSequenceFactory;
        }

        private IBootSequence _bootSequence;
        private IBootSequence BootSequence => _bootSequence ??= _bootSequenceFactory.New();

        public IEnumerable<TApplicationModule> Configure<TApplicationModule, TApplicationBuilder>(TApplicationBuilder appBuilder)
            where TApplicationModule : class, IApplicationModule<TApplicationBuilder>
            where TApplicationBuilder : IDotNetStackApplicationBuilder
        {
            var configuredModules = new List<TApplicationModule>();
            
            // NOTE: Please do no try and optimize. Especially not to put Config.EnabledModules into a variable before the loop or to use an foreach loop
            // because the collection is expanded inside the loop due to recursive nature of the stack.
            for (var i = 0; i < _moduleManager.EnabledModules.Count; i++)
            {
                if (_moduleManager.EnabledModules[i] is TApplicationModule module)
                {
                    module.Configure(appBuilder);
                    configuredModules.Add(module);
                }
            }

            return configuredModules;
        }

        public async Task StartAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule
        {
            await BootSequence.ForEachAsync<IApplicationModule>(module => module.StartAsync());
            await BootSequence.ForEachAsync<TApplicationModule>(module => module.StartAsync());
            
            _moduleManager.SetModuleState(ModuleState.Started);
        }

        public async Task StopAsync<TApplicationModule>()
            where TApplicationModule : IDotNetStackApplicationModule
        {
            await BootSequence.Reverse().ForEachAsync<TApplicationModule>(module => module.StopAsync());
            await BootSequence.Reverse().ForEachAsync<IApplicationModule>(module => module.StopAsync());
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }
    }
}
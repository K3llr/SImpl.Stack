using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Core;
using SImpl.DotNetStack.Extensions;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.Application
{
    public class ApplicationBootManager : IApplicationBootManager
    {
        private readonly IModuleManager _moduleManager;

        public ApplicationBootManager(IModuleManager moduleManager)
        {
            _moduleManager = moduleManager;
        }

        private IEnumerable<IApplicationModule> _bootSequence;
        private IEnumerable<IApplicationModule> BootSequence => _bootSequence ??= _moduleManager.BootSequence.Cast<IApplicationModule>();

        public void Configure(IDotNetStackApplicationBuilder appBuilder)
        {
            // NOTE: Please do no try and optimize. Especially not to put Config.EnabledModules into a variable before the loop or to use an foreach loop
            // because the collection is expanded inside the loop due to recursive nature of the stack.
            for (var i = 0; i < _moduleManager.EnabledModules.Count; i++)
            {
                var module = _moduleManager.EnabledModules[i] as IApplicationModule;
                module?.Configure(appBuilder);
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            BootSequence.ForEach<IServicesCollectionConfigureModule>(module => module.ConfigureServices(serviceCollection));
        }

        public async Task StartAsync()
        {
            await BootSequence.ForEachAsync<IApplicationModule>(module => module.StartAsync());
            
            _moduleManager.SetModuleState(ModuleState.Started);
        }

        public async Task StopAsync()
        {
            await BootSequence.ForEachAsync<IApplicationModule>(module => module.StopAsync());
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }
    }
}
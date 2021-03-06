using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Host.Builders;
using SImpl.Modules;
using SImpl.Runtime.Extensions;

namespace SImpl.Runtime.Core
{
    public class HostBootManager : IHostBootManager
    {
        private readonly IModuleManager _moduleManager;
        private readonly IBootSequenceFactory _bootSequenceFactory;

        public HostBootManager(IModuleManager moduleManager, IBootSequenceFactory bootSequenceFactory)
        {
            _moduleManager = moduleManager;
            _bootSequenceFactory = bootSequenceFactory;
        }

        private IBootSequence _bootSequence1;
        private IBootSequence BootSequence => _bootSequence1 ??= _bootSequenceFactory.New();

        public  IEnumerable<IPreInitModule> PreInit()
        {
            var preInitModules = new List<IPreInitModule>();
            
            // NOTE: Please do no try and optimize the following loop. Especially not to put Config.EnabledModules into a variable before the loop or to use an foreach loop
            // because the collection is expanded inside the loop due to recursive nature of the stack.
            for (var i = 0; i < _moduleManager.EnabledModules.Count; i++)
            {
                if (_moduleManager.EnabledModules[i] is IPreInitModule module)
                {
                    module.PreInit();
                    preInitModules.Add(module);
                }
            }

            return preInitModules;
        }

        public void ConfigureServices(ISImplHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((hostBuilderContext, services) =>
            {
               
                BootSequence.ForEach<IServicesCollectionConfigureModule>(module => module.ConfigureServices(services));
            });
        }

        public void ConfigureHostBuilder(ISImplHostBuilder hostBuilder)
        {
            BootSequence.ForEach<IHostBuilderConfigureModule>(module =>
            {
                module.ConfigureHostBuilder(hostBuilder);
            });
            
            // we need re-calc
            _bootSequence1 = _bootSequenceFactory.New();
        }

        public void ConfigureHost(IHost host)
        {
            BootSequence.ForEach<IHostConfigureModule>(module =>
            {
                module.ConfigureHost(host);
            });
            
            _moduleManager.SetModuleState(ModuleState.Configured);
        }

        public async Task StartAsync(IHost host)
        {
            await BootSequence.ForEachAsync<IStartableModule>(module => module.StartAsync(host));

            _moduleManager.SetModuleState(ModuleState.Started);
        }

        public async Task StopAsync(IHost host)
        {
            await BootSequence.Reverse().ForEachAsync<IStartableModule>(module => module.StopAsync(host));
            
            _moduleManager.SetModuleState(ModuleState.Stopped);
        }

      
    }
}
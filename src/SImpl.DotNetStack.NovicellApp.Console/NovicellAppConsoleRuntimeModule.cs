using System;
using System.Threading.Tasks;
using Novicell.App.AppBuilders;
using Novicell.App.Console;
using Novicell.App.Console.Configuration;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost;
using SImpl.DotNetStack.Modules;
using SImpl.DotNetStack.Modules.Dependencies;

namespace SImpl.DotNetStack.App
{
    [DependsOn(typeof(GenericHostStackApplicationModule))]
    public class NovicellAppConsoleRuntimeModule : IApplicationModule
    {
        private readonly Action<IConsoleAppBuilder> _configure;

        public NovicellAppConsoleRuntimeModule(Action<IConsoleAppBuilder> configure)
        {
            _configure = configure;
        }

        public void Configure(IDotNetStackApplicationBuilder builder)
        {
            
        }

        public string Name => "Novicell App Console Runtime Module";

        public Task StartAsync()
        {
            ConsoleBootManager.Boot(new ConfigurableStartup(appBuilder =>
            {
                appBuilder.UseNovicellConsoleApp(consoleApp =>
                {
                    _configure?.Invoke(consoleApp);
                });
            }));

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            ConsoleBootManager.UnloadApp();
            
            return Task.CompletedTask;
        }
    }
}
using System;
using System.Threading.Tasks;
using Novicell.App.AppBuilders;
using Novicell.App.Console;
using Novicell.App.Console.Configuration;
using SImpl.Application.Builders;
using SImpl.Hosts.GenericHost;
using SImpl.Modules;

namespace SImpl.DotNetStack.App
{
    [DependsOn(typeof(GenericHostApplicationModule))]
    public class NovicellAppConsoleRuntimeModule : IApplicationModule
    {
        private readonly Action<IConsoleAppBuilder> _configure;

        public NovicellAppConsoleRuntimeModule(Action<IConsoleAppBuilder> configure)
        {
            _configure = configure;
        }

        public void Configure(ISImplApplicationBuilder builder)
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
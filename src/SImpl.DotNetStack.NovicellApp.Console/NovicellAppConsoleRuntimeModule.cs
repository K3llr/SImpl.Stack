using Novicell.App.Console;
using Novicell.App.Console.Configuration;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Modules;

namespace SImpl.DotNetStack.App
{
    public class NovicellAppModule : IApplicationConfigureModule
    {
        public string Name => "Novicell App Stack Module";

        public void ConfigureApplication(IDotNetStackApplicationBuilder builder)
        {
            ConsoleBootManager.Boot(new ConfigurableStartup())
        }
    }
}
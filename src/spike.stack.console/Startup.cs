using Novicell.App.Console;
using Novicell.App.Console.AppBuilders;
using Novicell.App.Console.Extensions.Configuration;
using spike.stack.module;

namespace spike.stack.console
{
    public class Startup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseConsoleStackApp(consoleApp =>
            {
                consoleApp.UseTest01Module();
            });
        }
    }
}
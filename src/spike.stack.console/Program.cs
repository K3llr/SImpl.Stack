using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Novicell.App.Hosted.GenericHost.Extensions;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureGenericStackHost(consoleBuilder => // IConsoleHostBuilder
                {
                    consoleBuilder.UseStartup<Startup>();
                })
                .Build();
                
            await host.RunAsync();
        }
    }
}

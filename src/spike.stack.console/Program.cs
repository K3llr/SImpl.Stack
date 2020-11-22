using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Novicell.App.Console.Extensions.Configuration;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureConsoleStackHost(consoleBuilder => // IConsoleHostBuilder
                {
                    consoleBuilder.UseStartup<Startup>();
                    consoleBuilder.ConfigureServices(services =>
                    {
                        // Add services to service collection
                    });
                })
                .Build()
                .RunAsync();
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.GenericHost.Extensions;
using SImpl.DotNetStack.HostBuilders;
using spike.stack.module;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .AddDotNetStack(args, stackHostBuilder =>
                {
                    stackHostBuilder.Use<Level2aModule>();
                    stackHostBuilder.Use<Level2bModule>();
                    stackHostBuilder.Use<Level1Module>();
                    stackHostBuilder.Use<RootModule>();
                    
                    stackHostBuilder.UseDotNetStackTestModule();
                    
                    //stackHostBuilder.UseDependencyInjection();
                    
                    stackHostBuilder.UseGenericHostStackApp(genericStackHostBuilder => 
                    {
                        genericStackHostBuilder.UseStartup<Startup>();
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss.fff ";
                    });
                })
                .Build()
                .RunAsync();
        }
    }
}

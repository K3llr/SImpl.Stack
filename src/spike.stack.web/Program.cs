using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.HostBuilders;
using spike.stack.module;

namespace spike.stack.web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // TODO: Check whether .NET 5 runtime executes all ConfigureServices (eg. startup.cs + below) or just one of them as is the case with the Configure method

            await Host.CreateDefaultBuilder(args)
                .AddDotNetStack(args, stack =>
                {
                    stack.ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
                        
                    stack.UseDotNetStackTestModule();
                })
                .Build()
                .RunAsync();
        }
    }
}

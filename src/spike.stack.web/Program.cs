using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Runtime.HostBuilders;
using spike.stack.app.Application;
using spike.stack.app.Domain;
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
                    stack.ConfigureServices(services =>
                    {
                        // Works
                        /*services.AddSingleton<IGreetingAppService, GreetingAppService>();
                        services.AddSingleton<IGreetingService, SpanishGreetingService>();
                        services.AddHostedService<GreetingHostedService>();*/
                    });
                    
                    stack.ConfigureWebHostDefaults(webHostBuilder =>
                    {
                        webHostBuilder.UseStartup<Startup>();
                    });
                }).ConfigureServices(services =>
                {
                    // Works
                    /*services.AddSingleton<IGreetingAppService, GreetingAppService>();
                    services.AddSingleton<IGreetingService, SpanishGreetingService>();
                    services.AddHostedService<GreetingHostedService>();*/
                })
                .Build()
                .RunAsync();
        }
    }
}

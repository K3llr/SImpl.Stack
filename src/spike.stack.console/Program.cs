using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Extensions;
using SImpl.DotNetStack.Runtime.HostBuilders;
using spike.stack.app.Application;
using spike.stack.app.Domain;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .AddDotNetStack(args, stack =>
                {
                   /* stack.ConfigureServices(services =>
                    {
                        // Works
                        services.AddScoped<IGreetingAppService, GreetingAppService>();
                        services.AddScoped<IGreetingService, SpanishGreetingService>();
                        services.AddHostedService<GreetingHostedService>();
                    });*/
                    
                    stack.ConfigureGenericHost(genericHostBuilder => 
                    {
                        genericHostBuilder.UseStartup<Startup>();
                        /*genericHostBuilder.ConfigureServices(services =>
                        {
                            // Works
                            services.AddScoped<IGreetingAppService, GreetingAppService>();
                            services.AddScoped<IGreetingService, SpanishGreetingService>();
                            services.AddHostedService<GreetingHostedService>();
                        });*/
                    });
                })
                /*.ConfigureServices(services =>
                {
                    // Works
                    services.AddScoped<IGreetingAppService, GreetingAppService>();
                    services.AddScoped<IGreetingService, SpanishGreetingService>();
                    services.AddHostedService<GreetingHostedService>();
                })*/
                /*.ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss.fff ";
                    });
                })*/
                .Build()
                .RunAsync();
        }
    }
}

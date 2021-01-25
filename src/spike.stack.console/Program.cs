using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Hosts.GenericHost.Extensions;
using spike.stack.app.Application;
using spike.stack.app.Domain;
using spike.stack.module;
using HostBuilderExtensions = SImpl.Stack.Runtime.HostBuilders.HostBuilderExtensions;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await HostBuilderExtensions.SImply(Host.CreateDefaultBuilder(args), args, stack =>
                {
                   /* stack.ConfigureServices(services =>
                    {
                        // Works
                        services.AddScoped<IGreetingAppService, GreetingAppService>();
                        services.AddScoped<IGreetingService, SpanishGreetingService>();
                        services.AddHostedService<GreetingHostedService>();
                    });*/

                    // stack.ConfigureGenericHostStackApp<Startup>();
                    
                    DotNetStackHostBuilderExtensions.ConfigureGenericHostStackApp(stack, host => 
                    { 
                        host.UseStartup<Startup>();
                        host.ConfigureStackApplication(app =>
                        {
                            DotNetStackApplicationBuilderExtensions.UseStackAppModule<TestStackedApplicationModule>(app);
            
                            DotNetStackApplicationBuilderExtensions.UseStackAppModule<ApplicationTestModule>(app);
                            //stack.UseApplicationTestModule();
                            GenericHostApplicationBuilderExtensions.UseGenericHostStackAppModule<GenericHostApplicationTestModule>(app);
                            //stack.UseGenericHostApplicationTestModule();*/
                        });
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

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.HostBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.Extensions;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;
using SImpl.DotNetStack.Runtime.HostBuilders;
using SImpl.DotNetStack.Runtime.ApplicationBuilders;
using spike.stack.app.Application;
using spike.stack.app.Domain;
using spike.stack.module;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .SImply(args, stack =>
                {
                   /* stack.ConfigureServices(services =>
                    {
                        // Works
                        services.AddScoped<IGreetingAppService, GreetingAppService>();
                        services.AddScoped<IGreetingService, SpanishGreetingService>();
                        services.AddHostedService<GreetingHostedService>();
                    });*/

                    // stack.ConfigureGenericHostStackApp<Startup>();
                    
                    stack.ConfigureGenericHostStackApp(host => 
                    { 
                        host.UseStartup<Startup>();
                        host.ConfigureStackApplication(app =>
                        {
                            app.UseStackAppModule<TestStackedApplicationModule>();
            
                            app.UseStackAppModule<ApplicationTestModule>();
                            //stack.UseApplicationTestModule();
                            app.UseGenericHostStackAppModule<GenericHostApplicationTestModule>();
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

using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SImpl.Hosts.GenericHost;
using SImpl.Runtime;
using spike.stack.module;

namespace spike.stack.console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .SImplify(args, stack =>
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
                        host.ConfigureApplication(app =>
                        {
                            app.UseAppModule<TestStackedApplicationModule>();
            
                            app.UseAppModule<ApplicationTestModule>();
                            //stack.UseApplicationTestModule();
                            app.UseGenericHostAppModule<GenericHostApplicationTestModule>();
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

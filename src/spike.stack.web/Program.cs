using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SImpl.Stack.Hosts.WebHost.Extensions;
using SImpl.Stack.Runtime.HostBuilders;

namespace spike.stack.web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            // TODO: Check whether .NET 5 runtime executes all ConfigureServices (eg. startup.cs + below) or just one of them as is the case with the Configure method

            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseStartup<Startup>();
                })
                .SImply(args, stack =>
                {
                    stack.ConfigureWebHostStackApp(host =>
                    {
                        host.UseStartup<Startup>();
                        
                        /*host.ConfigureStackApplication(app =>
                        {
                            app.UseWebHostStackAppModule<GreetingsWebModule>();
                
                            app.UseStackAppModule<TestStackedApplicationModule>();
                        
                            app.UseStackAppModule<ApplicationTestModule>();
                            //stack.UseApplicationTestModule();
                
                            // NOTE: Below is not supposed to work
                            //stack.UseGenericHostStackAppModule<GenericHostApplicationTestModule>();
                            //stack.UseGenericHostApplicationTestModule();
                
                            app.UseWebHostStackAppModule<WebHostApplicationTestModule>();
                            //stack.UseWebApplicationTestModule();
                        });*/
                    });
                    
                    /*stack.ConfigureWebHostStackApp(app =>
                    {
                        app.UseWebHostStackAppModule<GreetingsWebModule>();
                
                        app.UseStackAppModule<TestStackedApplicationModule>();
                        
                        app.UseStackAppModule<ApplicationTestModule>();
                        //stack.UseApplicationTestModule();
                
                        // NOTE: Below is not supposed to work
                        //stack.UseGenericHostStackAppModule<GenericHostApplicationTestModule>();
                        //stack.UseGenericHostApplicationTestModule();
                
                        app.UseWebHostStackAppModule<WebHostApplicationTestModule>();
                        //stack.UseWebApplicationTestModule();
                    });*/
                    /*stack.ConfigureServices(services =>
                    {
                        // Works
                        services.AddSingleton<IGreetingAppService, GreetingAppService>();
                        services.AddSingleton<IGreetingService, SpanishGreetingService>();
                        services.AddHostedService<GreetingHostedService>();
                    });*/
                    //stack.Use<WebHostStackApplicationModule>();
                    /*stack.ConfigureWebHostDefaults(webHostBuilder =>
                    {
                        webHostBuilder.UseStartup<Startup>();
                    });*/
                })
                
                /*.ConfigureServices(services =>
                {
                    // Works
                    services.AddSingleton<IGreetingAppService, GreetingAppService>();
                    services.AddSingleton<IGreetingService, SpanishGreetingService>();
                    services.AddHostedService<GreetingHostedService>();
                })*/
                .Build()
                .RunAsync();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;
using SImpl.Stack.Hosts.GenericHost.Startup;
using spike.stack.module;

namespace spike.stack.console
{
    public class Startup : IGenericHostStackApplicationStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Works
            //services.AddScoped<IGreetingAppService, GreetingAppService>();
            //services.AddScoped<IGreetingService, SpanishGreetingService>();
            //services.AddHostedService<GreetingHostedService>();
        }

        public void ConfigureStackApplication(IGenericHostApplicationBuilder builder)
        {
            builder.UseStackAppModule<TestStackedApplicationModule>();
            
            builder.UseStackAppModule<ApplicationTestModule>();
            //stack.UseApplicationTestModule();
            builder.UseGenericHostStackAppModule<GenericHostApplicationTestModule>();
            //stack.UseGenericHostApplicationTestModule();
            
            // NOTE: Below is not supposed to work
            //stack.UseWebHostStackAppModule<WebHostApplicationTestModule>();
            //stack.UseWebApplicationTestModule();

            /*stackBuilder.UseNovicellConsoleApp(consoleApp =>
            {
                //consoleApp.UseHybridTestModule();
                //consoleApp.Use<LegacyStackModule>();

                consoleApp.UseDependencyInjection(di =>
                {
                    di.UseGenericHost();
                    
                    di.RegisterServices(container =>
                    {
                        container.RegisterHostedService<GreetingHostedService>();
                        
                        container.Register<IGreetingAppService, GreetingAppService>(Lifestyle.Singleton);
                        container.Register<IGreetingService, SpanishGreetingService>(Lifestyle.Singleton);
                    });
                });
            });*/
        }
    }
}